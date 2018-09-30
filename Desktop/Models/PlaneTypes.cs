using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
    public class PlaneTypes
    {
        //Combo box feltöltése
        public static object CreateComboBox()
        {
            string[] strArray =
                {
                "Airbus A380",
                "Boeing 747",
                "Boeing 777",
                "Antonov 124",
                //További repülő típusok
            };
            return strArray;
        }
    }
}
