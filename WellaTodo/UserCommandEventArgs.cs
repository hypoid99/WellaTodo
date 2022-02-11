using System;

namespace WellaTodo
{
    public class UserCommandEventArgs : EventArgs
    {
        private string commandName;
        private object argument;

        public UserCommandEventArgs(string commandName, object argument)
        {
            this.commandName = commandName;
            this.argument = argument;
        }

        public UserCommandEventArgs(string commandName)
        {
            this.commandName = commandName;
        }

        public string CommandName
        {
            get { return commandName; }
        }

        public object CommandArgument
        {
            get { return argument; }
        }
    }
}
