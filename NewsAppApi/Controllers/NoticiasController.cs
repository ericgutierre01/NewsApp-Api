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
using NewsAppApi.Entities.Data;
using NewsAppApi.ViewModel;

namespace NewsAppApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {

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
                var imagenes = new List<string>();

                if (tema.Equals("COVID-19"))
                {
                    imagenes.Add("https://scontent.fhex4-1.fna.fbcdn.net/v/t1.0-9/97056685_10157095018841811_4295269760481886208_o.jpg?_nc_cat=100&_nc_sid=a26aad&_nc_eui2=AeHZDLBAfLQ_Ylyt45YoTy6m0XWyXyFV2TDRdbJfIVXZMH_ALYcxoFv2fPPORXDHZ71oDHjNqRky4e2Y-jyBtJFc&_nc_ohc=Xc7S5y5Jo6oAX8bRVgw&_nc_ht=scontent.fhex4-1.fna&oh=c7ac02f0b47866772751fdbc3406a82a&oe=5EE31554");
                    imagenes.Add("https://scontent.fhex4-1.fna.fbcdn.net/v/t1.0-9/97249918_10157095019001811_3063169898118119424_o.jpg?_nc_cat=1&_nc_sid=a26aad&_nc_eui2=AeHswBAU9pEBBf9sxRnrp1EbCLfMImWtG6AIt8wiZa0boHHrmCRzEWABa0nFAVMavEJUuI_7Um_WJSeKIZZIte-c&_nc_ohc=p1GbRDqpOrIAX8bAoO7&_nc_ht=scontent.fhex4-1.fna&oh=6ee8475a58a69e519e05e36452c06277&oe=5EE05FF4");
                    imagenes.Add("https://scontent.fhex4-1.fna.fbcdn.net/v/t1.0-9/97153572_10157095019071811_1271163887001010176_o.jpg?_nc_cat=102&_nc_sid=a26aad&_nc_eui2=AeFKUSj3U-kuee98Gt-yqjItoJ3FvVkb7t6gncW9WRvu3o4UBVrx3m5nRdfIf81KLM-sr6z5Nx4GRkXRLrwn2zB9&_nc_ohc=I9DO-HWQp_8AX-VBn5X&_nc_ht=scontent.fhex4-1.fna&oh=ce4373f1f03d76486743599e8d07e6a7&oe=5EE33C6D");

                }


                return Ok(imagenes);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }

        }
    }
}