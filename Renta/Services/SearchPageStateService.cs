using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Services
{
    public class SearchPageStateService
    {
        public string _CategoryString { get; set; } = "0";
        public int PriceRangeStart { get; set; } = 0;
        public int PriceRangeEnd { get; set; } = 100;

        public string SelectedCity { get; set; }
    }
}
