using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class InputDto
    {
        public JRaw data { get; set; } = null!;
        /*public Input[] data;*/
    }
}
