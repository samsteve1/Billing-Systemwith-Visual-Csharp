using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthmarkStore.BLL
{
    class DeaCustBll
    {
        public int Id { get; set; }
        public string  Type { get; set; }
        public string Name { get; set; }
        public string  Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public DateTime Added_date { get; set; }
        public int Added_by { get; set; }
    }
}
