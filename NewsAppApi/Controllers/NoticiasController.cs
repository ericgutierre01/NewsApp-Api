using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NewsAppApi.Entities;
using NewsAppApi.Entities.Data;
using NewsAppApi.ViewModel;

namespace NewsAppApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly ApiContext _db;
        public NoticiasController(ApiContext context)
        {
            _db = context;
        }

        [HttpGet("{tema}")]
        [AllowAnonymous]
        public async Task<ActionResult> Noticias([FromRoute] string tema, string start)
        {
            try
            {
                HttpClient cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Add("User-Agent","aspnetcoremaster.com");

                cliente.BaseAddress = new Uri("https://www.googleapis.com");

                string jsonFromGoogle = "";

                /*if(jsonFromGoogle.Equals("Deportes"))
                     jsonFromGoogle = await cliente.GetStringAsync("/customsearch/v1?key=AIzaSyBZLm0x5sKGSI0fex-zYdAcyuPe68OvjBk&cx=007020137502699158807:z6srbycjumm&q=" + tema + "&sort=date&start=" + start);
                else*/
                 jsonFromGoogle = await cliente.GetStringAsync("/customsearch/v1?key=AIzaSyBZLm0x5sKGSI0fex-zYdAcyuPe68OvjBk&cx=007020137502699158807:twljqntbg80&q="+tema+ "&sort=date&start="+start);

                Noticia noti = JsonSerializer.Deserialize<Noticia>(jsonFromGoogle);
                var noticias = new List<NoticiaViewModel>();
                var noticia = new NoticiaViewModel();
               foreach (ItemNoticia iten in noti.items)
                {
                    try
                    {
                        noticia = new NoticiaViewModel();
                        noticia.Titulo = iten.title;
                        noticia.Descripcion = iten.snippet;
                        noticia.Link = iten.link;
                        noticia.LinkMostrar = iten.displayLink;
                        noticia.Imagen = iten.pagemap.cse_thumbnail[0].src;
                        noticias.Add(noticia);

                    }
                    catch
                    {

                    }

                }

                return Ok(noticias);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }

        }


        [HttpGet("GetHotImagenes{tema}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetHotImagenes([FromRoute] string tema)
        {
            try
            {
                var imagenes = _db.Imagenes.Where(x => x.ImaTitulo == tema);

                return Ok(imagenes);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }

        }

        [HttpPost("PostHotImagenes")]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] ImagenHot model)
        {
            try
            {
                model.ImaId = 0;
                _db.Add(model);

                await _db.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ActualizarHotImagenes")]
        [AllowAnonymous]
        public async Task<ActionResult> Put([FromBody] ImagenHot model)
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