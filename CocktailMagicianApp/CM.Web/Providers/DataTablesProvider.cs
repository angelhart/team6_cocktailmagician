using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers
{
    [BindProperties]
    public class DataTablesProvider<T>
    {
        public DataTablesProvider()
        {

        }

        public DataTablesProvider(int draw, int recordsTotal, int recordsFiltered, ICollection<T> data)
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

        public static DataTablesProvider<T> CreateResponse(int draw, int recordsTotal, int recordsFiltered, ICollection<T> data)
        {
            return new DataTablesProvider<T>(draw, recordsTotal, recordsFiltered, data);
        }
    }
}
