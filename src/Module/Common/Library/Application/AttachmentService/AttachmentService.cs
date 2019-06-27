using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Options;
using Nm.Lib.Utils.Core.Result;
using Nm.Lib.Utils.Mvc.Extensions;
using Nm.Lib.Utils.Mvc.Helpers;
using Nm.Module.Common.Application.AttachmentService.ResultModels;
using Nm.Module.Common.Application.AttachmentService.ViewModels;
using Nm.Module.Common.Domain.Attachment;
using Nm.Module.Common.Domain.Attachment.Models;
using Nm.Module.Common.Domain.AttachmentOwner;
using Nm.Module.Common.Domain.MediaType;
using Nm.Module.Common.Infrastructure.Options;
using Nm.Module.Common.Infrastructure.Repositories;

namespace Nm.Module.Common.Application.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly LoginInfo _loginInfo;
        private readonly ModuleCommonOptions _moduleCommonOptions;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IAttachmentRepository _repository;
        private readonly IAttachmentOwnerRepository _ownerRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;

        public AttachmentService(IAttachmentRepository repository, IUnitOfWork<CommonDbContext> uow, IAttachmentOwnerRepository ownerRepository, LoginInfo loginInfo, IOptionsMonitor<CommonOptions> optionsMonitor, IOptionsMonitor<ModuleCommonOptions> moduleCommonOptionsMonitor, FileUploadHelper fileUploadHelper, IMediaTypeRepository mediaTypeRepository, IMapper mapper)
        {
            _repository = repository;
            _uow = uow;
            _ownerRepository = ownerRepository;
            _loginInfo = loginInfo;
            _moduleCommonOptions = moduleCommonOptionsMonitor.CurrentValue;
            _fileUploadHelper = fileUploadHelper;
            _mediaTypeRepository = mediaTypeRepository;
            _mapper = mapper;
        }

        public async Task<IResultModel> Query(AttachmentQueryModel model)
        {
            var result = new QueryResultModel<AttachmentEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Upload(AttachmentUploadModel model, HttpRequest request)
        {
            var uploadModel = new FileUploadModel
            {
                Request = request,
                FormFile = model.File,
                RootPath = _moduleCommonOptions.UploadPath,
                Module = "Common",
                Group = Path.Combine("Attachment", model.Module, model.Group)
            };

            //附件存储路径/Common/Attachment/{所属模块名称}/{所属分组模块}

            var result = await _fileUploadHelper.Upload(uploadModel);

            if (result.Successful)
            {
                var file = result.Data;
                var entity = new AttachmentEntity
                {
                    Module = model.Module,
                    Group = model.Group,
                    FileName = file.FileName,
                    SaveName = file.SaveName,
                    Ext = file.Ext,
                    Md5 = file.Md5,
                    Path = file.Path,
                    FullPath = Path.Combine(file.Path, file.SaveName),
                    Size = file.Size.Size,
                    SizeCn = file.Size.ToString()
                };

                var mediaType = await _mediaTypeRepository.GetByExt(file.Ext);
                if (mediaType != null)
                {
                    entity.MediaType = mediaType.Value;
                }

                _uow.BeginTransaction();

                if (await _repository.AddAsync(entity))
                {
                    //如果需要授权访问附件，需要添加拥有者关联信息
                    if (!model.Auth || await _ownerRepository.AddAsync(new AttachmentOwnerEntity { AttachmentId = entity.Id, AccountId = _loginInfo.AccountId }))
                    {
                        _uow.Commit();

                        var resultModel = _mapper.Map<AttachmentUploadResultModel>(entity);
                        var url = request.GetHost($"/common/attachment/download/{entity.Id}");
                        resultModel.Url = new Uri(url).ToString();

                        return ResultModel.Success(resultModel);
                    }
                }
            }

            return ResultModel.Failed("上传失败");
        }

        public async Task<IResultModel<FileDownloadModel>> Download(Guid id)
        {
            var result = new ResultModel<FileDownloadModel>();

            var attachment = await _repository.GetAsync(id);
            if (attachment == null)
                return result.Failed("附件不存在");

            if (attachment.Auth)
            {
                var has = await _ownerRepository.Exist(new AttachmentOwnerEntity { AccountId = _loginInfo.AccountId, AttachmentId = id });
                if (!has)
                {
                    return result.Failed("您无权访问该附件");
                }
            }

            var filePath = Path.Combine(_moduleCommonOptions.UploadPath, attachment.FullPath);
            if (!File.Exists(filePath))
                return result.Failed("文件不存在");

            return result.Success(new FileDownloadModel(filePath, attachment.FileName, attachment.MediaType));
        }

        public async Task<IResultModel<FileDownloadModel>> Export(Guid id)
        {
            var result = new ResultModel<FileDownloadModel>();

            var attachment = await _repository.GetAsync(id);
            if (attachment == null)
                return result.Failed("附件不存在");

            var filePath = Path.Combine(_moduleCommonOptions.UploadPath, attachment.FullPath);
            if (!File.Exists(filePath))
                return result.Failed("文件不存在");

            return result.Success(new FileDownloadModel(filePath, attachment.FileName, attachment.MediaType));
        }
    }
}
