using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Core.Caching;

namespace Sample.Repository
{
    public class BaseRepository :  IDisposable
    {
        protected PracticeContext db = new PracticeContext();
        
        public BaseRepository()
        {
            //var m = db.ObjectStateManager;        
            if(ConfigHelper.GetBoolValue("DbLogging",false))
              db.Database.Log = (m) => System.Diagnostics.Trace.WriteLine(m);
        }

        #region IDisposable Members

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {                                
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
