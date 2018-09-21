using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects
{
    public class ReserveSeat_DTO
    {
        public ReserveSeat_DTO(long pId, long sId)
        {
            planeId = pId;
            seatId = sId;
            user = "abc";
        }

        //A foglalandó hely adatai
        //Pl. Melyik repülőn, hanyas hely
        private long planeId;
        private long seatId;
        //A foglaló azonosítója(neve)
        private String user;
    }
}
