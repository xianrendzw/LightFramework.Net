namespace LightFramework.Components
{
    using System;
    using System.ComponentModel;

    public class WorkerCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object _argument;

        public WorkerCompletedEventArgs(Exception ex, bool canceled, object userState)
            : base(ex, canceled, userState)
        {
        }

        public WorkerCompletedEventArgs(Exception ex, bool canceled, object userState, object argument)
            : base(ex, canceled, userState)
        {
            this._argument = argument;
        }

        public object Argument
        {
            get { return this._argument; }
        }
    }
}

