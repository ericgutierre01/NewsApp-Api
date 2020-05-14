using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Entities.Data
{
    public class ItemNoticia
    {
        public string kind { get; set; }
        public string title { get; set; }
        public string htmlTitle { get; set; }
        public string link { get; set; }
        public string displayLink { get; set; }
        public string snippet { get; set; }
        public string htmlSnippet { get; set; }
        public string cacheId { get; set; }
        public string formattedUrl { get; set; }
        public string htmlFormattedUrl { get; set; }

        public PageMap pagemap { get; set; }
    }
}
