using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Entities.Data
{
    public class Noticia
    {
        public string kind { get; set; }
        public List<ItemNoticia> items { get; set; }

        // public string htmlTitle { get; set; }
        // public string htmlSnippet { get; set; }
        //public string cse_image { get; set; }*

        /*public string login { get; set; }
        public string name { get; set; }*/

    }
}
