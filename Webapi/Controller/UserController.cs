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
using Entity;
using AutoMapper;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Webapi.Controller
{
    [CustomAuthorize]
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
            var user = new User { userId=1,sex=2,address="mintcode"};

            var userId = req.userId;
            var resUser = Mapper.Map<ResUserModel>(user);
            return resUser;
        }

        [HttpGet]
        public IHttpActionResult CreateAuthCode()
        {
            HttpResponseMessage res = new HttpResponseMessage();
            var imgStream = _userService.CreateAuthPic();
            var contentType = "image/png";
            return new ImgResult(imgStream,contentType);
        }
    }
}
