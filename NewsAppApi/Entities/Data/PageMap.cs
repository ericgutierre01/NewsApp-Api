using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAppApi.Entities.Data
{
    public class PageMap
    {
        public List<Cse_image> cse_thumbnail { get; set; }
        public List<MetaTags> metatags { get; set; }
    }
}
