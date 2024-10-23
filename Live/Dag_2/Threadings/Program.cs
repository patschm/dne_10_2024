


using System.Threading.Channels;

namespace Threadings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DemoSynchronous();
            //DemoThread();
            //DemoApm();
            //DemoTask();
            //DemoTaskVervolg();
            //DemoExceptions();
            //CancellationTokenSource nikko = new CancellationTokenSource();
            //DemoDuurtTeLang(nikko.Token);
            //nikko.CancelAfter(7000);
            //DemoDeHippeVariant();
            //DemoMeerdereTakenAsync();
            DemoLocking();

            Console.WriteLine("Einde programma");
            Console.ReadLine();
        }

        static object stokje = new object();
        
        private static void DemoLocking()
        {
            int counter = 0;

            ThreadPool.SetMinThreads(20, 0);
           for(int i = 0; i < 20; i++)
            {
                Task.Run(() => {
                    
                    lock (stokje)
                    {
                        //Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}");
                        int tmp = counter;
                        Task.Delay(1000).Wait();
                        tmp++;
                        counter = tmp;
                    }
                    Console.WriteLine(counter);
                });
            }
        }

        private static async Task DemoMeerdereTakenAsync()
        {
            int a = 0;
            int b = 0;

            //var zaklamp1 = new AutoResetEvent(false);
            //var zaklamp2 = new AutoResetEvent(false);

            var t1 = Task.Run(() =>
            {
                Task.Delay(2000).Wait();
                a = 20;
                //zaklamp1.Set();
            });
            var t2 = Task.Run(() =>
            {
                Task.Delay(3000).Wait();
                b = 10;
               // zaklamp2.Set();
            });

            //WaitHandle.WaitAny([zaklamp1, zaklamp2]);

            //Task.WaitAll(t1, t2 ); // Blocking
            await Task.WhenAll(t1, t2);
            int result = a + b;
            Console.WriteLine(result);
        }

        private static void DemoDuurtTeLang(CancellationToken bommetje)
        {
            //CancellationTokenSource nikko = new CancellationTokenSource();

            //CancellationToken bommetje = nikko.Token;
            Task.Run(() =>
            {
                for (int i = 0; ; i++)
                {
                    if (bommetje.IsCancellationRequested)
                    {
                        Console.WriteLine("Kaboom");
                        break;
                    }
                    //bommetje.ThrowIfCancellationRequested();
                    Console.WriteLine(i);
                    Task.Delay(500).Wait();
                }
            });

           // nikko.CancelAfter(5000);
        }
        private static void DemoExceptions()
        {
            Task t = new Task(() => {
                Console.WriteLine("We doen iets");
                throw new Exception("ooops");
            });
            t.ContinueWith(pt =>
            {
                if (pt.Exception != null)
                {
                    Console.WriteLine(pt.Status);
                    Console.WriteLine(pt.Exception.InnerException);
                }
            });
            t.Start();
            // Werkt niet
            //try
            //{
            //    t.Start();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
        }

        private static async void DemoDeHippeVariant()
        {
            var t1 = new Task<int>(() => LongAdd(3, 4));
            t1.Start();

            int res = await t1;
            Console.WriteLine($"Na taak 1: {res}");

            res = await Task.Run(() => LongAdd(5, 6));
            Console.WriteLine($"Na taak 2: {res}");
            // Console.WriteLine(t1.Result);

            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine("Dit gaat fout...");
                    throw new Exception("Ooops");
                });
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);           
            }
        }

        private static void DemoTaskVervolg()
        {
            var t1 = new Task<int>(() => LongAdd(3,4));
            
            t1.Start();

            t1.ContinueWith(prevTask => { Console.WriteLine($"{prevTask.Result}"); });
            t1.ContinueWith(t =>
            {
                var result = LongAdd(14, 5);
                return result;
            }).ContinueWith(pt => Console.WriteLine($"Weer klaar {pt.Result}"));

        }

        private static void DemoTask()
        {
            Task t1 = new Task(() =>
            {
                var result = LongAdd(14, 5);
                Console.WriteLine(result);
            });
            t1.Start();

            var t2 = Task.Run(() =>
            {
                var result = LongAdd(14, 5);
                Console.WriteLine(result);
            });
        }

        private static void DemoApm()
        {
            Func<int, int, int> del = LongAdd;

            IAsyncResult ar = del.BeginInvoke(4, 5, ar => {
                int result = del.EndInvoke(ar);
                Console.WriteLine(result);
            }, null);
         
        }
        private static void DemoThread()
        {
            var t1 = new Thread(() =>
            {
                var result = LongAdd(14, 5);
                Console.WriteLine(result);
            });
            t1.Start();
        }

        private static void DemoSynchronous()
        {
            var result = LongAdd(4, 5);
            Console.WriteLine(result);
        }

        static int LongAdd(int a, int b)
        {
            Task.Delay(5000).Wait();
            return a + b;
        }
    }
}
