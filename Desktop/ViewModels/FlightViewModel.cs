using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Desktop.Models;
using Desktop.Services;

using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    public class FlightViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private Stack<ICommandBase> commandStack = new Stack<ICommandBase>();
        private Stack<ICommandBase> undoStack = new Stack<ICommandBase>();
        private bool undoEnabled = false;
        private bool redoEnabled = false;

        public bool UndoEnabled
        {
            get { return undoEnabled; }
            set { undoEnabled = value; RaisePropertyChanged("UndoEnabled"); }
        }

        public bool RedoEnabled
        {
            get { return redoEnabled; }
            set { redoEnabled = value; RaisePropertyChanged("RedoEnabled"); }
        }

        //Adatforrás
        public ObservableCollection<Flight> Source
        {
            get
            {
                return DataService.FlightList;
            }
        }

        internal void ExecuteCommand(ICommandBase cmd)
        {
            cmd.Execute();
            commandStack.Push(cmd);
            undoStack.Clear();

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
        }
    }
}
