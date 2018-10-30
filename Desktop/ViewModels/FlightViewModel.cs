using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Desktop.Models;
using Desktop.Services;

using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    public class FlightViewModel : ViewModelBase
    {
        private Stack<CommandBase> commandStack = new Stack<CommandBase>();
        private Stack<CommandBase> undoStack = new Stack<CommandBase>();
        private bool undoEnabled = false;
        private bool redoEnabled = false;
        private bool isFlightSelected = false;
        private long nextId = 0;

        public long NextId
        {
            get
            {
                if (nextId == 0) nextId = FlightsDataService.MaxId;
                nextId++;
                return nextId;
            }
        }

        public bool UndoEnabled
        {
            get { return undoEnabled; }
            private set { undoEnabled = value; RaisePropertyChanged("UndoEnabled"); }
        }

        public bool RedoEnabled
        {
            get { return redoEnabled; }
            private set { redoEnabled = value; RaisePropertyChanged("RedoEnabled"); }
        }

        public bool IsFlightSelected
        {
            get { return isFlightSelected; }
            set { isFlightSelected = value; RaisePropertyChanged("IsFlightSelected"); }
        }

        //Adatforrás
        public ObservableCollection<Flight> Source
        {
            get
            {
                return FlightsDataService.FlightList;
            }
        }

        internal void ExecuteCommand(CommandBase cmd)
        {
            cmd.Execute();
            commandStack.Push(cmd);
            undoStack.Clear();

            IsFlightSelected = false;

            //Parancs után lehet undo, de nem lehet redo
            UndoEnabled = true;
            RedoEnabled = false;
        }

        internal void UnExecuteCommand()
        {
            var cmd = commandStack.Pop();
            cmd.UnExecute();
            undoStack.Push(cmd);

            //Ha üres, akkor nem lehet undo
            if (commandStack.Count == 0)
            {
                UndoEnabled = false;
            }
            RedoEnabled = true;

            IsFlightSelected = false;
        }

        internal void ReExecuteCommand()
        {
            var cmd = undoStack.Pop();
            cmd.Execute();
            commandStack.Push(cmd);

            //Ha üres, akkor nem lehet redo
            if (undoStack.Count == 0)
            {
                RedoEnabled = false;
            }

            IsFlightSelected = false;
        }
    }
}
