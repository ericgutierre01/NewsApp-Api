using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private readonly Helpers.AppSettings _appSettings;
        public NoticiasController(IOptions<Helpers.AppSettings> appSettings, ApiContext context)
        {
            _appSettings = appSettings.Value;
            _db = context;
        }

        [HttpGet("{tema}")]
        [AllowAnonymous]
        public async Task<ActionResult> Get([FromRoute] string tema, string start)
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
                var imagenes = _db.Imagenes.Where(x => x.ImaTitulo == tema);//.Select(x => Path.Combine(_appSettings.DomainImg, "hots", x.ImaPath));

                await imagenes.ForEachAsync(x =>
                {
                    x.ImaPath = x.ImaIsLink ? x.ImaPath : Path.Combine(_appSettings.DomainImg, "hots", x.ImaPath);
                });
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

                if (!model.ImaIsLink)
                {
                    var location = Path.Combine(_appSettings.PathImg, "hots");
                    var image = Helpers.DataImage.TryParse(model.ImaPath);
                    var fileName = $"{Helpers.Utils.GenerateGuid()}.{image.Extesion}";

                    //Guardar Imagenes Fisicamente!.-
                    System.IO.File.WriteAllBytes(Path.Combine(location, fileName), image.RawData);

                    model.ImaPath = fileName;
                }
                _db.Add(model);

                await _db.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("BorrarHotImagenes")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete([FromBody] ImagenHot model)
        {
            try
            {
                _db.Remove(model);

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