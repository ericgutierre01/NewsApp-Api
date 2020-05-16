using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Entities.Data
{
    public class ImagenHot
    {
        [Key]
        public int ImaId { get; set; }
        public string ImaPath { get; set; }
        public string ImaTitulo { get; set; }
    }
}
