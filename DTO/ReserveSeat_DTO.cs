using System;

namespace DTO
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
