using System;

namespace DTO
{
    public class ListFlights_DTO
    {
        //A listázandó repülők paraméterei
        //Pl.: Csak a szabad hellyel rendelkezők
        public bool OnlyAvailable { get; set; }
        //Csak az adott dátumon (az idópont nem számít)
        public DateTime AtDate { get; set; }
        //Indulás
        public String From { get; set; }
        //Érkezés
        public String To { get; set; }

        //Stb.
    }
}
