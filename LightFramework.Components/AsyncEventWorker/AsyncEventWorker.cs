using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace LightFramework.Components
{
    public class AsyncEventWorker : Component
    {
        private delegate void WorkerThreadDelegate(AsyncOperation asyncOp, object userState, object argument);

        private IContainer components;
        private HybridDictionary userStateToLifetime;
        private readonly WorkerThreadDelegate workerThread;

        public event DoWorkEventHandler DoWork;
        public event WorkerCompletedEventHandler Completed;
        public event ProgressChangedEventHandler ProgressChanged;

        public AsyncEventWorker()
        {
            this.userStateToLifetime = new HybridDictionary();
            this.workerThread = new WorkerThreadDelegate(this.WorkerThreadStart);
            this.components = new Container();
        }

        public AsyncEventWorker(IContainer container)
            : this()
        {
            container.Add(this);
        }

        public void RunAsync()
        {
            this.RunAsync(Guid.NewGuid().ToString());
        }

        public void RunAsync(object userState)
        {
            this.RunAsync(userState, null);
        }

        public void RunAsync(object userState,object argument)
        {
            AsyncOperation asyncOp = AsyncOperationManager.CreateOperation(userState);
            lock (this.userStateToLifetime.SyncRoot)
            {
                if (this.userStateToLifetime.Contains(userState))
                {
                    throw new ArgumentException(string.Format("{0},已经存在.", userState), "userState");
                }
                this.userStateToLifetime[userState] = asyncOp;
            }

            this.workerThread.BeginInvoke(asyncOp, userState, argument, null, null);
        }

        private void WorkerThreadStart(AsyncOperation asyncOp, object userState, object argument)
        {
            Exception error = null;
            bool cancelled = false;
            DoWorkEventArgs e = new DoWorkEventArgs(userState, argument);

            try
            {
                this.OnDoWork(e);
                if (e.Cancel) cancelled = true;
            }
            catch (Exception ex)
            {
                error = ex;
            }

            lock (userStateToLifetime.SyncRoot)
            {
                userStateToLifetime.Remove(asyncOp.UserSuppliedState);
            }

            WorkerCompletedEventArgs arg = new WorkerCompletedEventArgs(error, cancelled, asyncOp.UserSuppliedState, argument);
            asyncOp.PostOperationCompleted(this.SendOrPostWorkerCompleted, arg);
        }

        public void ReportProgress(int percentProgress,object userState)
        {
            this.ReportProgress(percentProgress, userState, null);
        }

        public void ReportProgress(int percentProgress, object userState, object argument)
        {
            ProgressChangedEventArgs args = new ProgressChangedEventArgs(percentProgress, userState, argument);
            AsyncOperation asyncOp = this.userStateToLifetime[userState] as AsyncOperation;
            if (asyncOp == null) return;
            asyncOp.Post(this.SendOrPostReportProgress, args);
        }

        public void CancelAsync(object userState)
        {
            if (this.userStateToLifetime[userState] is AsyncOperation)
            {
                lock (this.userStateToLifetime.SyncRoot)
                {
                    this.userStateToLifetime.Remove(userState);
                }
            }
        }

        public void Clear()
        {
            lock (this.userStateToLifetime.SyncRoot)
            {
                this.userStateToLifetime.Clear();
            }
        }

        public bool Exists(object userState)
        {
            lock (this.userStateToLifetime.SyncRoot)
            {
                return this.userStateToLifetime.Contains(userState);
            }
        }

        public int ThreadCount
        {
            get { return this.userStateToLifetime.Count; }
        }

        protected virtual void OnDoWork(DoWorkEventArgs e)
        {
            if (this.DoWork != null)
                this.DoWork(this, e);
        }

        protected virtual void OnCompleted(WorkerCompletedEventArgs e)
        {
            if (this.Completed != null)
                this.Completed(this, e);
        }

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if (this.ProgressChanged != null)
                this.ProgressChanged(this, e);
        }

        private void SendOrPostReportProgress(object state)
        {
            ProgressChangedEventArgs e = state as ProgressChangedEventArgs;
            this.OnProgressChanged(e);
        }

        private void SendOrPostWorkerCompleted(object state)
        {
            WorkerCompletedEventArgs e = state as WorkerCompletedEventArgs;
            this.OnCompleted(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

