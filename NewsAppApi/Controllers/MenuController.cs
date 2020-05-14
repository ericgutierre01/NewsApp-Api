using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAppApi.Entities.Data;

namespace NewsAppApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Menu()
        {
            var listMenu = new List<Menu>
            {
                new Menu{Id ="1",Titulo="Hoy", Imagen="f1ea", IsHot =false },
                new Menu{Id ="2",Titulo="COVID-19", Imagen="f06d", IsHot =true },
                new Menu{Id ="3",Titulo="Politica", Imagen="f2b5" , IsHot =false},
                new Menu{Id ="4",Titulo="Elecciones", Imagen="f756", IsHot =false },
                new Menu{Id ="5",Titulo="Corona Virus", Imagen="f96c", IsHot =false },
                new Menu{Id ="6",Titulo="Deportes", Imagen="f434", IsHot =false }
            };

            return Ok(listMenu);
        }
    }
}