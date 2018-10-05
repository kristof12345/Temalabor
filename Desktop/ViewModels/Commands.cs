using Desktop.Models;
using Desktop.Services;
using System;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter = null);

        public abstract void UnExecute();
    }

    public class AddCommand : CommandBase
    {
        private long id;
        private DateTime date;
        private string dep;
        private string des;
        private string type;

        public AddCommand(long id, DateTimeOffset date, TimeSpan time, string dep, string des, string type)
        {
            this.id = id;
            this.date = CombineDateAndTime(date, time);
            this.dep = dep;
            this.des = des;
            this.type = type;
        }

        public override void Execute(object parameter = null)
        {
            DataService.AddFlightAsync(id, date, dep, des, type);
        }

        public override void UnExecute()
        {
            DataService.DeleteFlightAsync(new Flight(id));
        }

        //Segédfüggvény a dátum előállításához
        private DateTime CombineDateAndTime(DateTimeOffset date, TimeSpan time)
        {
            DateTime tempTime = date.UtcDateTime;
            tempTime = tempTime.Date + time;
            return tempTime;
        }
    }

    public class DeleteCommand : CommandBase
    {
        private Flight flight;
        public DeleteCommand(Flight flight)
        {
            this.flight = flight;
        }
        public override void Execute(object parameter = null)
        {
            DataService.DeleteFlightAsync(flight);
        }

        public override void UnExecute()
        {
            DataService.AddFlightAsync(flight);
        }
    }

    public class UpdateCommand : CommandBase
    {
        Flight oldFlight;
        Flight newFlight;
        public UpdateCommand(Flight flight, Flight old)
        {
            this.newFlight = flight;
            this.oldFlight = old;
        }

        public override void Execute(object parameter = null)
        {
            DataService.UpdateFlightAsync(newFlight);
        }

        public override void UnExecute()
        {
            DataService.UpdateFlightAsync(oldFlight);
        }
    }
}
