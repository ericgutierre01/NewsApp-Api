using NewsAppApi.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.ViewModel
{
    public class MenuViewModel
    {
        public int menId { get; set; }
        public string menTitulo { get; set; }
        public string Imagen { get; set; }
        public bool menIsHot { get; set; }
        public int menEstado { get; set; }
        public List<ImagenHot> ImagenesHot{ get; set; }
    }
}
