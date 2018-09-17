using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects
{
    class Seat
    {
        //Automatikusan növekvő, egyedi ID
        private static int nextId = 1;
        private int id;
        private KeyValuePair<String, int> type = new KeyValuePair<String, int>("Abc", 5);

        public Seat()
        {
            IsReserved = false;
            id = nextId++;
        }
        
        //Foglalt-e a szék (csak lekérdezhető)
        public bool IsReserved { get; }
    }
}