using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers
{
    [BindProperties]
    public class AjaxProvider<T>
    {
        public AjaxProvider()
        {

        }

        public AjaxProvider(int draw, int recordsTotal, int recordsFiltered, ICollection<T> data)
        {
            this.draw = draw;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
            this.data = data;
        }

        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public ICollection<T> data { get; set; }

        public static AjaxProvider<T> CreateResponse(int draw, int recordsTotal, int recordsFiltered, ICollection<T> data)
        {
            return new AjaxProvider<T>(draw, recordsTotal, recordsFiltered, data);
        }
    }
}
