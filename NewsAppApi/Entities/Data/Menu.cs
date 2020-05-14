using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Entities.Data
{
    public class Menu
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public bool IsHot { get; set; }
    }
}
