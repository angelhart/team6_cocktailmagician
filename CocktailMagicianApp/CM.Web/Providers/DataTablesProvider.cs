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

        public DataTablesProvider(int draw, int recordsTotal, int recordsFiltered, ICollection<T> data, string role)
        {
            this.Draw = draw;
            this.RecordsTotal = recordsTotal;
            this.RecordsFiltered = recordsFiltered;
            this.Data = data;
            this.Role = role;
        }

        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public ICollection<T> Data { get; set; }
        public string Role { get; set; }

        public static DataTablesProvider<T> CreateResponse(int draw, int recordsTotal, int recordsFiltered, ICollection<T> data, string role = "")
        {
            return new DataTablesProvider<T>(draw, recordsTotal, recordsFiltered, data, role);
        }
    }
}
