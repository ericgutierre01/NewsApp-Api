using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAppApi.Entities;
using NewsAppApi.Entities.Data;

namespace NewsAppApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly ApiContext _db;
        public MenuController(ApiContext context)
        {
            _db = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Menu>>> Get()
        {
            /*var listMenu = new List<Menu>
            {
                new Menu{Id ="1",Titulo="Hoy", Imagen="f1ea", IsHot =false },
                new Menu{Id ="2",Titulo="COVID-19", Imagen="f06d", IsHot =true },
                new Menu{Id ="3",Titulo="Politica", Imagen="f2b5" , IsHot =false},
                new Menu{Id ="4",Titulo="Elecciones", Imagen="f756", IsHot =false },
                new Menu{Id ="5",Titulo="Corona Virus", Imagen="f96c", IsHot =false },
                new Menu{Id ="6",Titulo="Deportes", Imagen="f434", IsHot =false },
                new Menu{Id ="6",Titulo="Farandula", Imagen="f434", IsHot =false }
            };*/

            try
            {
                var listMenu = _db.Menus.Where(x => x.menEstado == 1);

                return Ok(listMenu);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] Menu model)
        {
            try
            {
                model.menId = 0;
                model.menEstado = 1;
                _db.Add(model);

                await _db.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Actualizar")]
        [AllowAnonymous]
        public async Task<ActionResult> Put([FromBody] Menu model)
        {
            try
            {
                _db.Update(model);

                await _db.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}