using System.ComponentModel;

namespace LightFramework.Components
{
    public class DoWorkEventArgs : System.ComponentModel.CancelEventArgs
    {
        private object _argument;
        private object _userState;

        public DoWorkEventArgs(object userState, object argument)
        {
            this._userState = userState;
            this._argument = argument;
        }

        public object Argument
        {
            get { return this._argument; }
        }

        public object UserState
        {
            get { return this._userState; }
            set { this._userState = value; }
        }
    }
}
