using Desktop.Models;
using Desktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    //Az ICommand már foglalt volt
    public interface ICommandBase
    {
        void Execute();
        void UnExecute();
    }

    public class AddCommand : ICommandBase
    {
        private DateTime date;
        private string dep;
        private string des;
        private string type;

        public AddCommand(DateTimeOffset date, TimeSpan time, string dep, string des, string type)
        {
            this.date = CombineDateAndTime(date, time);
            this.dep = dep;
            this.des = des;
            this.type = type;
        }

        public void Execute()
        {
            DataService.AddFlightAsync(date, dep, des, type);
        }

        public void UnExecute()
        {
            DataService.DeleteFlightAsync(new Flight(date, dep, des, type));
        }

        //Segédfüggvény a dátum előállításához
        private DateTime CombineDateAndTime(DateTimeOffset date, TimeSpan time)
        {
            DateTime tempTime = date.UtcDateTime;
            tempTime = tempTime.Date + time;
            return tempTime;
        }
    }

    public class DeleteCommand : ICommandBase
    {
        private Flight flight;
        public DeleteCommand(Flight flight)
        {
            this.flight = flight;
        }
        public void Execute()
        {
            DataService.DeleteFlightAsync(flight);
        }

        public void UnExecute()
        {
            DataService.AddFlightAsync(flight);
        }
    }
}
