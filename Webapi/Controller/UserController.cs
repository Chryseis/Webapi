using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Webapi.Model;
using IRepository;
using Infrastructure;

namespace Webapi.Controller
{
    public class UserController : BaseController
    {
        private IUser _userService;

        public UserController(IUser userService)
        {
            this._userService = userService;
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
            return user;
        }
    }
}
