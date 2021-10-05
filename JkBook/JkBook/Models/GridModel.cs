using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Models
{
    public class GridModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }
        public string Date { get; set; }
    }
    public class GridModelList
    {
        public List<GridModel> GridData { get; set; }
    }
}
