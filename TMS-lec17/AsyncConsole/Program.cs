using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var result = new Program().GetDelayedTimeAsync();
            //Console.WriteLine(result);
            //Console.WriteLine(new Program().GetDelayedTime());
            //Console.WriteLine("Helo testing");

            //Task.WaitAll(result);
            //Console.WriteLine(result.Result);




            //var t= new TaskBasedAsyncCode().GetCurrentTimeAsync();

            //Console.WriteLine(t.Result);
            Console.WriteLine("Starting Time : " + DateTime.Now.ToString("HH:mm:ss"));
            new Tea().MakeTea();
            Console.WriteLine("Ending Time : " + DateTime.Now.ToString("HH:mm:ss"));
            Console.WriteLine("TEsting");
            Console.WriteLine("Starting Time : "+DateTime.Now.ToString("HH:mm:ss"));
            var TRes = new TeaAsync().MakeTea();
            Console.WriteLine("Ending Time : " + DateTime.Now.ToString("HH:mm:ss"));
            Console.ReadLine();
        }
        public DateTime GetDelayedTime()
        {
            Thread.Sleep(10000);
            Console.WriteLine("Current Thread ID : "+Thread.CurrentThread.ManagedThreadId.ToString()); 
            return DateTime.Now;
        }
        public async Task<DateTime> GetDelayedTimeAsync()
        {
            await Task.Delay(10000);
            return DateTime.Now;
        }
        

    }
    public class EventBasedAysncCode
    { 
        public class GetCurrentTimeEventArgs : EventArgs {
            public Exception Error { get; set; }
            public bool Canceled { get; set; }
            public DateTime Result { get; set; }
            
        }
        public event EventHandler<GetCurrentTimeEventArgs> CurrentTimeCompleted;
        public void GetCurrentTimeAsyn() {
                        
        }

    }
    public class TaskBasedAsyncCode
    {
        public Task<DateTime> GetCurrentTimeAsync() { 
         var tcs= new TaskCompletionSource<DateTime>();
            var eap = new EventBasedAysncCode();
            eap.GetCurrentTimeAsyn();


            eap.CurrentTimeCompleted += (srouce, e) => { 
                if(e.Error!=null) tcs.TrySetException(e.Error);
                else if(e.Canceled) tcs.SetCanceled();
                else tcs.SetResult(e.Result);
            };
            return tcs.Task;
        }

        
    }

}
