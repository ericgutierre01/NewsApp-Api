using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Entities.Data
{
    public class Icono
    {
        [Key]
        public int IcoId { get; set; }
        public string IcoDescripcion { get; set; }
        public string IcoWeb { get; set; }
        public string IcoApp { get; set; }
    }
}
