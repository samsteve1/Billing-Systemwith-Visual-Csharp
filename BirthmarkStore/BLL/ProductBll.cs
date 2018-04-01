using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthmarkStore.BLL
{
    class ProductBll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public decimal Qty { get; set; }
        public DateTime Added_Date { get; set; }
        public int Added_By { get; set; }
    }
}
