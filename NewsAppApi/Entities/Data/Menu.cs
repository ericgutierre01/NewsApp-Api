using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Entities.Data
{
    public class Menu
    {
        [Key]
        public int menId { get; set; }
        public string menTitulo { get; set; }
        public string Imagen { get; set; }
        public bool menIsHot { get; set; }
        public int menEstado { get; set; }
    }
}
