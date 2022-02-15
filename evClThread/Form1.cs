using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Application Used Library
using System.Threading;
using System.Collections;
using System.Threading.Tasks;
using System.Net;

namespace evClThread
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        #region TPL coding
        internal static Queue qDlp = new Queue(); //Initializes a queue
        internal static Queue qSyncdDlp = Queue.Synchronized(qDlp); //Create a synchronized wrapper around the Queue (thread safe)

        internal static Stack stkDlp = new Stack(); //Initializes a Stack
        internal static Stack stkSyncdDlp = Stack.Synchronized(stkDlp); //Create a synchronized wrapper around the Stack (thread safe)         

        internal static ArrayList alDlp = new ArrayList(); //Initializes a Array List
        internal static ArrayList alSyncdDlp = ArrayList.Synchronized(alDlp); //Create a synchronized wrapper around the Array List (thread safe)         

        internal static SortedList slDlp = new SortedList(); //Initializes a Sorted List
        internal static SortedList slSyncdDlp = SortedList.Synchronized(slDlp); //Create a synchronized wrapper around the Sorted List (thread safe)         

        internal static int iTmr;
        internal static int iMaxQueue = 10000;


        private static void TaskRun(string sName, int iIterations)
        {
            for (int i = 0; i < iIterations; ++i)
            {
                qSyncdDlp.Enqueue("Task Run: " + sName + ": " + Convert.ToString(i));
            }
        }

        private void btnTPLStart_Click(object sender, EventArgs e)
        {
            /*
             * .NET 4 introduces the Task Parallel Library (TPL), a set of classes in the System.Thread
             * ing.Tasks namespace that help coordinate concurrent work. In some respects, the TPL
             * superficially resembles the thread pool, in that you submit small work items (or
             * tasks) and the TPL will take care of working out how many threads to use at once in
             * order to run your work. But the TPL provides various services not available through
             * direct use of the thread pool, especially in the areas of error handling, cancellation, and
             * managing relationships between tasks.
             */

            //Clear queue
            qSyncdDlp.Clear();
            lbDisplay.Items.Clear();

            //Task Parallel Library (TPL) Task factory
            Task tMonitor = Task.Factory.StartNew(() => TaskRun("TPL 1", 10000));
            Task tProtect = Task.Factory.StartNew(() => TaskRun("TPL 2", 10000));

            //Timer
            //tmrDlpMonitor.Enabled = true;            

            Task.WaitAll(tMonitor, tProtect);

            DisplaySyncdQueue(qSyncdDlp);
        }

        private void btnTPLContinue_Click(object sender, EventArgs e)
        {
            /*
             * A continuation is a task that gets invoked when another tasks completes.† The Task
             * class provides a ContinueWith method that lets you provide the code for that continuation
             * task. It requires a delegate that takes as its single argument the task that just
             * completed. ContinueWith offers overloads that allow the delegate to return a value (in
             * which case the continuation task will be another Task<TResult>), or not to return a
             * value (in which case the continuation task will just be a Task). ContinueWith returns the
             * Task object that represents the continuation.
             * */

            //Clear queue
            qSyncdDlp.Clear();
            lbDisplay.Items.Clear();

            //Continuation Task Parallel Library (TPL) Task factory
            Task tDlp = Task.Factory.StartNew(() => TaskRun("Task", 10000))
                                    .ContinueWith(t1 => TaskRun("t1", 10000))
                                    .ContinueWith(t2 => TaskRun("t2", 10000));
            tDlp.Wait();
            DisplaySyncdQueue(qSyncdDlp);
        }

        private void tmrDlpMonitor_Tick(object sender, EventArgs e)
        {
            if (iTmr < iMaxQueue)
            {
                qSyncdDlp.Enqueue("Timer: " + Convert.ToString(iTmr));
                ++iTmr;
            }
        }

        private void btnQueueRead_Click(object sender, EventArgs e)
        {
            try
            {
                DisplaySyncdQueue(qSyncdDlp);
            }
            catch (Exception exp)
            {
                lbDisplay.Items.Add(exp.Message);
            }
        }

        private void btnQueueClear_Click(object sender, EventArgs e)
        {
            qSyncdDlp.Clear();
            lbDisplay.Items.Add("Synchronized queue cleared.");
        }

        /// <summary>
        /// Display a Queue on list box
        /// </summary>
        /// <param name="mySyncdQueue"></param>
        private void DisplaySyncdQueue(Queue mySyncdQueue)
        {
            lbDisplay.Items.Clear();
            lbDisplay.Items.Add("Queue Count: " + qSyncdDlp.Count);

            if (mySyncdQueue.IsSynchronized == true)
                lbDisplay.Items.Add("Queue is synchronzied");

            foreach (object obj in mySyncdQueue)
                lbDisplay.Items.Add(obj);
        }

        private void btnThreadWaitFor_Click(object sender, EventArgs e)
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
            try
            {
                qSyncdDlp.Clear();

                evClThreadMonitorWaitForIt waiter = new evClThreadMonitorWaitForIt();

                SynchronizationContext originalContext = SynchronizationContext.Current;

                ThreadStart tWork = delegate
                {
                    originalContext.Post(delegate
                    {
                        lbDisplay.Items.Add("Synchronization context demo...");
                    }, null);
                        qSyncdDlp.Enqueue("Thread running...");
                        Thread.Sleep(1000);
                        qSyncdDlp.Enqueue("Notifying");
                        waiter.GoNow();
                        qSyncdDlp.Enqueue("Notified");
                        Thread.Sleep(1000);
                        qSyncdDlp.Enqueue("Thread exiting...");
                };

                Thread t = new Thread(tWork);
                qSyncdDlp.Enqueue("Thread monitor WaitForIt demo... Starting new thread");
                t.Start();
                qSyncdDlp.Enqueue("Waiting for thread to get going");
                waiter.WaitUntilReady();
                qSyncdDlp.Enqueue("Wait over");

                Thread.Sleep(1000);
                DisplaySyncdQueue(qSyncdDlp);
            }
            catch (Exception exp)
            {
                lbDisplay.Items.Add(exp.Message);
            }
        }

        private void btnTPLAPM_Click(object sender, EventArgs e)
        {
            /*
             * The Asynchronous Programming Model (APM) is a pattern that many asynchronous APIs in the .NET Framework conform to.
             * 
             * TaskFactory and TaskFactory<TResult> provide various overloads of a FromAsync
             * method. You can pass this the Begin and End methods from an APM implementation,
             * along with the arguments you’d like to pass, and it will return a Task or Task<TRe
             * sult> that executes the asynchronous operation, instead of one that invokes a delegate.
            */
            try
            {
                qSyncdDlp.Clear();
                lbDisplay.Items.Clear();

                TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

                Task t = Task<IPHostEntry>.Factory.FromAsync(Dns.BeginGetHostEntry, Dns.EndGetHostEntry, "oreilly.com", null)
                        .ContinueWith((task) => UpdateUi(task.Result.AddressList[0].ToString()), uiScheduler);

                
            }
            catch (AggregateException exp)
            {                
                qSyncdDlp.Enqueue(exp.Message);
            }
            catch (TaskSchedulerException exp)
            {
                qSyncdDlp.Enqueue(exp.Message);
            }
            catch (Exception exp)
            {
                qSyncdDlp.Enqueue(exp.Message);
            }
            finally 
            {               
                
            }
        }
        #endregion

        #region TPL Scheduler Demo
        private void btnTPLScheduler_Click(object sender, EventArgs e)
        {
            //The TaskScheduler class is responsible for working out when and how to execute tasks.
            //If you don’t specify a scheduler, you’ll end up with the default one, which uses the
            //thread pool. But you can provide other schedulers when creating tasks—both
            //StartNew and ContinueWith offer overloads that accept a scheduler. The TPL offers a
            //scheduler that uses the SynchronizationContext, which can run tasks on the UI thread.

            try
            {
                qSyncdDlp.Clear();
                lbDisplay.Items.Clear();

                TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

                Task t = Task<string>.Factory.StartNew(GetData)
                        .ContinueWith((task) => UpdateUi(task.Result), uiScheduler);

            }
            catch (Exception exp)
            {
                qSyncdDlp.Enqueue(exp.Message);
            }
        }

        private string GetData()
        {
            try
            {
                WebClient w = new WebClient();
                return w.DownloadString("http://oreilly.com/");
            }
            catch (Exception exp)
            {
                qSyncdDlp.Enqueue(exp.Message);
                return exp.Message;
            }
        }

        private void UpdateUi(string info)
        {
            lbDisplay.Items.Add(info);
        }
        #endregion

        #region TPL Cancellation Demo
        private CancellationTokenSource cancelSource;

        private void TPLCancellation_Click(object sender, EventArgs e)
        {
            /*
             * Cancellation of asynchronous operations is surprisingly tricky. There are lots of awkward
             * race conditions to contend with. The operation you’re trying to cancel might
             * already have finished by the time you try to cancel it. Or if it hasn’t it might have gotten
             * beyond the point where it is able to stop, in which case cancellation is doomed to fail.
             * Or work might have failed, or be about to fail when you cancel it. And even when
             * cancellation is possible, it might take awhile to do. Handling and testing every possible
             * combination is difficult enough when you have just one operation, but if you have
             * multiple related tasks, it gets a whole lot harder.
             * 
             * In fact, cancellation isn’t very effective in this example because this particular task
             * consists of code that makes a single blocking method call. Cancellation will usually do
             * nothing here in practice—the only situation in which it would have an effect is if the
             * user managed to click Cancel before the task had even begun to execute. This illustrates
             * an important issue: cancellation is never forced—it uses a cooperative approach, because
             * the only alternative is killing the thread executing the work. And while that would
             * be possible, forcibly terminating threads tends to leave the process in an uncertain
             * state—it’s usually impossible to know whether the thread you just zapped happened
             * to be in the middle of modifying some shared state. Since this leaves your program’s
             * integrity in doubt, the only thing you can safely do next is kill the whole program, which
             * is a bit drastic. So the cancellation model requires cooperation on the part of the task
             * in question. The only situation in which cancellation would have any effect in this
             * particular example is if the user managed to click the Cancel button before the task had
             * even begun.
             * 
             * If you have divided your work into numerous relatively short tasks, cancellation is more
             * useful—if you cancel tasks that have been queued up but not yet started, they will never
             * run at all. Tasks already in progress will continue to run, but if all your tasks are short,
             * you won’t have to wait long. If you have long-running tasks, however, you will need
             * to be able to detect cancellation and act on it if you want to handle cancellation swiftly.
             * This means you will have to arrange for the code you run as part of the tasks to have
             * access to the cancellation token, and they must test the IsCancellationRequested property
             * from time to time.
             * 
             * Cancellation isn’t the only reason a task or set of tasks might stop before finishing—
             * things might be brought to a halt by exceptions.
             */
            try
            {
                qSyncdDlp.Clear();
                lbDisplay.Items.Clear();

                cancelSource = new CancellationTokenSource();

                TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

                Task t = Task<string>.Factory.StartNew(GetData, cancelSource.Token)
                            .ContinueWith((task) => UpdateUiCancel(task.Result), uiScheduler);

            }
            catch (Exception exp)
            {
                qSyncdDlp.Enqueue(exp.Message);
            }
        }

        private void btnCancelTPLNow_Click(object sender, EventArgs e)
        {
            if (cancelSource != null)
            {
                cancelSource.Cancel();
            }
        }

        private void UpdateUiCancel(string info)
        {
            cancelSource = null;
            lbDisplay.Items.Add(info);
        }
        #endregion

        #region TPL Task Coordination with Monitor WaitForIt

        evClThreadMonitorWaitForIt TaskWaiter = new evClThreadMonitorWaitForIt();
        private int iRace = 0;

        private void btnTaskCoor_Click(object sender, EventArgs e)
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

            //Clear queue
            qSyncdDlp.Clear();

            //Task Parallel Library (TPL) Task factory
            Task tCoor1 = Task.Factory.StartNew(() => TaskCoordination("Task 1", 10000));
            Task tCoor2 = Task.Factory.StartNew(() => TaskCoordination("Task 2", 10000));

            Task.WaitAll(tCoor1, tCoor2);

            DisplaySyncdQueue(qSyncdDlp);
        }

        private void TaskCoordination(string sName, int iIterations)
        {
            for (int i = 0; i < iIterations; ++i)
            {
                if (iRace == 0)
                {
                    TaskWaiter.GoNow();
                    iRace++;
                }
                TaskWaiter.GoNow();
                qSyncdDlp.Enqueue("Task Coordination: " + sName + ": " + Convert.ToString(i));
                TaskWaiter.WaitUntilReady();
            }
        }
        #endregion


    }
}
