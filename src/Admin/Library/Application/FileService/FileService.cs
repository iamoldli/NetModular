using System;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Enums;
using NetModular.Module.Admin.Application.FileService.ViewModels;
using NetModular.Module.Admin.Domain.File;
using NetModular.Module.Admin.Domain.File.Models;
using NetModular.Module.Admin.Domain.FileOwner;
using NetModular.Module.Admin.Domain.Mime;
using NetModular.Module.Admin.Infrastructure.Repositories;
using FileInfo = NetModular.Lib.Utils.Core.Files.FileInfo;

namespace NetModular.Module.Admin.Application.FileService
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _repository;
        private readonly IFileStorageProvider _fileStorageProvider;
        private readonly IMimeRepository _mimeRepository;
        private readonly IFileOwnerRepository _ownerRepository;
        private readonly AdminDbContext _dbContext;

        public FileService(IFileRepository repository, IFileStorageProvider fileStorageProvider, IMimeRepository mimeRepository, IFileOwnerRepository ownerRepository, AdminDbContext dbContext)
        {
            _repository = repository;
            _fileStorageProvider = fileStorageProvider;
            _mimeRepository = mimeRepository;
            _ownerRepository = ownerRepository;
            _dbContext = dbContext;
        }

        public async Task<IResultModel> Query(FileQueryModel model)
        {
            var result = new QueryResultModel<FileEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            foreach (var entity in result.Rows)
            {
                entity.Url = _fileStorageProvider.GetUrl(entity.FullPath, entity.AccessMode);
            }
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(FileUploadModel model, FileInfo fileInfo)
        {
            var entity = new FileEntity
            {
                ModuleCode = model.ModuleCode,
                Group = model.Group,
                AccessMode = model.AccessMode,
                FileName = fileInfo.FileName,
                SaveId = fileInfo.SaveName.Split('.')[0],
                Path = fileInfo.Path,
                FullPath = fileInfo.FullPath,
                Ext = fileInfo.Ext,
                Size = fileInfo.Size.Size,
                SizeName = fileInfo.Size.ToString(),
                Md5 = fileInfo.Md5
            };

            var mime = await _mimeRepository.Get(fileInfo.Ext.ToLower());
            if (mime != null)
            {
                entity.Mime = mime.Value;
            }

            using var uow = _dbContext.NewUnitOfWork();
            var result = await _repository.AddAsync(entity, uow);
            if (result)
            {
                #region ==绑定文件拥有者==

                if (model.AccessMode == FileAccessMode.Auth && model.Accounts != null && model.Accounts.Any())
                {
                    foreach (var accountId in model.Accounts.Split(','))
                    {
                        await _ownerRepository.AddAsync(new FileOwnerEntity
                        {
                            SaveId = entity.SaveId,
                            AccountId = Guid.Parse(accountId)
                        }, uow);
                    }
                }

                #endregion

                #region ==OSS上传==

                var fileObject = new FileObject
                {
                    ModuleCode = entity.ModuleCode,
                    Group = entity.Group,
                    AccessMode = entity.AccessMode,
                    PhysicalPath = model.PhysicalPath,
                    FileInfo = fileInfo
                };

                await _fileStorageProvider.Upload(fileObject);

                entity.Url = fileInfo.Url;

                #endregion

                uow.Commit();

                return ResultModel.Success(entity);
            }
            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("文件不存在");

            var result = await _repository.SoftDeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> HardDelete(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("文件不存在");

            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                var fileObject = new FileObject
                {
                    ModuleCode = entity.ModuleCode,
                    Group = entity.Group,
                    AccessMode = entity.AccessMode,
                    FileInfo = FileEntity2FileInfo(entity)
                };

                await _fileStorageProvider.Delete(fileObject);
            }
            return ResultModel.Result(result);
        }

        public async Task<IResultModel<FileEntity>> Get(int id)
        {
            var result = new ResultModel<FileEntity>();
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return result.Failed("文件不存在");

            entity.Url = _fileStorageProvider.GetUrl(entity.FullPath, entity.AccessMode);

            return result.Success(entity);
        }

        public async Task<IResultModel> Get(string fullPath)
        {
            var saveId = FullPath2SaveId(fullPath);
            var entity = await _repository.GetBySaveId(saveId);
            if (entity == null)
                return ResultModel.Failed("文件不存在");

            entity.Url = _fileStorageProvider.GetUrl(entity.FullPath, entity.AccessMode);

            return ResultModel.Success(entity);
        }

        public async Task<IResultModel<FileEntity>> Download(string fullPath, Guid accountId)
        {
            var result = new ResultModel<FileEntity>();
            var saveId = FullPath2SaveId(fullPath);
            var entity = await _repository.GetBySaveId(saveId);
            if (entity == null)
                return result.Failed("文件不存在");

            if (entity.AccessMode == FileAccessMode.Auth)
            {
                var isOwner = await _ownerRepository.Exist(new FileOwnerEntity { AccountId = accountId, SaveId = entity.SaveId });
                if (!isOwner)
                    return result.Failed("无权访问");
            }

            entity.Url = _fileStorageProvider.GetUrl(entity.FullPath, entity.AccessMode);

            return result.Success(entity);
        }

        private FileInfo FileEntity2FileInfo(FileEntity entity)
        {
            return new FileInfo(entity.FileName, entity.Size)
            {
                Ext = entity.Ext,
                Md5 = entity.Md5,
                Path = $"{entity.Path}.{entity.Ext}",
                SaveName = entity.SaveId
            };
        }

        private string FullPath2SaveId(string fullPath)
        {
            var arr = fullPath.Split('/', '.');
            if (arr.Length < 6)
                throw new Exception("无效的地址");

            return fullPath.Split('/', '.')[5];
        }
    }
}
