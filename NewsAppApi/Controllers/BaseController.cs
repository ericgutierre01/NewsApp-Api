using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NewsAppApi.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
        public int UsuId { get; set; }
        public string UsuMaster { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ClaimsIdentity claims = (ClaimsIdentity)User.Identity;
            if (claims.Claims.Count() > 0)
                UsuId = Convert.ToInt32(claims.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}