using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Application Used Libraries
using System.Threading;

namespace evClThread
{
    /*
     * Monitors can help with this—as well as protecting shared state, they also provide a
     * way to discover when that state may have changed. The Monitor class provides a Wait
     * method that operates in conjunction with either a method called Pulse or the related
     * PulseAll. A thread that is waiting for something to change can call Wait, which will
     * block until some other thread calls Pulse or PulseAll. You must already hold the lock
     * on the object you pass as an argument to Wait, Pulse, or PulseAll. Calling them without
     * possessing the lock will result in an exception.
     */

    /// <summary>
    /// This class will replace Thread.Sleep. 
    /// Never use Thread.Sleep to try to solve ordering problems in production code. It's not a dependable or efficient tchnique.
    /// Uses this class to make the coordination problems more visible, but while it can be used to amplify or explore problems in examples, 
    /// you cannot rely on it, because it makes no guarantees—making one thread sleep for a while to give another thread a chance to 
    /// catch up does not guarantee that the other thread will catch up, particularly on systems that experience heavy load.
    /// </summary>
    public class evClThreadMonitorWaitForIt
    {
        private bool canGo;
        private object lockObject = new object();
        public void WaitUntilReady()
        {
            lock (lockObject)
            {
                while (!canGo)
                {
                    Monitor.Wait(lockObject);
                }
            }
        }
        public void GoNow()
        {
            lock (lockObject)
            {
                canGo = true;
                // Wake me up, before you go go.
                Monitor.PulseAll(lockObject);
            }
        }
    }
}
