using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Web;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("权限接口")]
    public class PermissionController : Web.ModuleController
    {
        private readonly PermissionCollection _collection;

        public PermissionController(PermissionCollection collection)
        {
            _collection = collection;
        }

        [HttpGet]
        [Description("查询")]
        public IResultModel Query([BindRequired]string moduleCode)
        {
            return ResultModel.Success(_collection.Query(moduleCode));
        }

        [HttpGet]
        [Description("权限树")]
        public IResultModel Tree()
        {
            return ResultModel.Success(_collection.Tree);
        }

        [HttpGet]
        [Description("根据编码查询")]
        public IResultModel QueryByCodes([FromQuery] List<string> codes)
        {
            return ResultModel.Success(_collection.Query(codes));
        }
    }
}
