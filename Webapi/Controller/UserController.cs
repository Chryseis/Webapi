using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Webapi.Model;
using IRepository;
using Infrastructure;
using log4net;
using Autofac;
using Autofac.Features.AttributeFilters;

namespace Webapi.Controller
{
    [Authorize(Roles="admin")]
    public class UserController : BaseController
    {
        private IUser _userService;

        private ILog _logger;

        public UserController(IUser userService,[KeyFilter("GlobalLog")] ILog logger)
        {
            this._userService = userService;
            this._logger = logger;
        }
        

        private List<ResUserModel> userList = new List<ResUserModel>
        {
            new ResUserModel{userId="1",userName="allen",sex=1,address="mintcode"},
            new ResUserModel{userId="2",userName="helen",sex=2,address="2dfire"}
        };

        [HttpGet]
        public ResUserModel GetUser([FromUri]ReqUserModel req)
        {
            var userId = req.userId;
            var user = userList.Where(t => t.userId == userId).First();
            _userService.GetUser();
            _logger.Info("哈哈哈");
            return user;
        }
    }
}
