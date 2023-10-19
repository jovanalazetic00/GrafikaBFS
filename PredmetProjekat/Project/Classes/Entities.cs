using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Classes
{
    public class Entities
    {
        public static List<PowerEntity> PowerEntities {  get; set; } = new List<PowerEntity>();
        public static List<LineEntity> Lines { get; set; } = new List<LineEntity>();
    }
}
