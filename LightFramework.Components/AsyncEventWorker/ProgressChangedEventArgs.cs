namespace LightFramework.Components
{
    public class ProgressChangedEventArgs : System.ComponentModel.ProgressChangedEventArgs
    {
        private object _argument;

        public ProgressChangedEventArgs(int progressPercentage, object userState)
            : base(progressPercentage, userState)
        {
        }

        public ProgressChangedEventArgs(int progressPercentage, object userState, object argument)
            : base(progressPercentage, userState)
        {
            this._argument = argument;
        }

        public object Argument
        {
            get { return this._argument; }
        }
    }
}

