using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generated;
namespace DataLayer.Entities
{

    //[UseRepository]
    [JsonSerializable]
    public class College
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CollegeId { get; set; }
        public College College { get; set; }
    }
}
