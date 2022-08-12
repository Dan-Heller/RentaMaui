using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class FilterPageViewModel
    {

        public List<string> Regions { get; set; }

        public FilterPageViewModel()
        {
            FetchRegionsFromString();
        }

        private void FetchRegionsFromString()
        {
            string RegionsStr = "Ashkelon \r\nBeer Sheva \r\nBethlehem \r\nGolan \r\nJenin \r\nHasharon \r\nHebron \r\nHadera \r\nHolon \r\nHaifa \r\nTulkarm \r\nJericho \r\nJerusalem \r\nKinneret \r\nNazareth \r\nAcre \r\nAfula \r\nPetah Tikva \r\nSafed \r\nRamallah \r\nRehovot \r\nRamla \r\nRamat Gan \r\nNablus \r\nTel Aviv";
            var stringArr = RegionsStr.Split("\r\n");
            Regions = stringArr.ToList();
        }
    }
}
