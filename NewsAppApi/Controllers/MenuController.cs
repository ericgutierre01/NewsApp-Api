using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAppApi.Entities;
using NewsAppApi.Entities.Data;
using NewsAppApi.ViewModel;

namespace NewsAppApi.Controllers
{
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class MenuController : BaseController
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MenuViewModel>> GetById(int id)
        {
            try
            {
                var menu = _db.Menus.SingleOrDefault(x => x.menId ==id);

                if (menu == null)
                    return BadRequest("No se encontro ningun titular");

                var CurrenMenu = new MenuViewModel()
                {
                    menTitulo = menu.menTitulo,
                    menEstado = menu.menEstado,
                    menId = menu.menId,
                    menIsHot = menu.menIsHot,
                    Imagen = menu.Imagen,
                };
                CurrenMenu.ImagenesHot = _db.Imagenes.Where(i => i.ImaTitulo == menu.menTitulo).ToList();
                return Ok(CurrenMenu);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("Iconos")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Icono>>> GetIconos()
        {

            try
            {
                var listIcono = _db.Iconos;

                return Ok(listIcono);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("Iconos{icoId}")]
        [AllowAnonymous]
        public async Task<ActionResult<Icono>> GetIconoById(int icoId)
        {

            try
            {
                var listIcono = _db.Iconos.SingleOrDefault(x=>x.IcoId == icoId);
                return Ok(listIcono);
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