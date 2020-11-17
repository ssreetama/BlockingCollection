using Queues.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Queues
{
    public class globalMethod
    {
        private static globalMethod instance = null;
        private static readonly object padlock = new object();
        public static BlockingCollection<Request> rq = new BlockingCollection<Request>(10);
        static Semaphore se = new Semaphore(1, 10);
        public static int addRequestFromWeb()
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new globalMethod();
                    }
                    Task t1 = new Task(() => { instance.synchronisedBlock(); });
                    t1.Start();
                }
            }
            return instance.addRequest();
        }

        public int addRequest()
        {
            Random r = new Random();
            int n = r.Next();
            Request b = new Request("Hi", n, n++);
            bool added = rq.TryAdd(b);
            if (added)
                return 1;
            else
                return -1;
        }
        public static int addrequesttocollection(Request r)
        {
            bool add = rq.TryAdd(r);
            if (add)
                return 1;
            else
                return -1;
        }
        public static BlockingCollection<Request> blockingcollectionreturn()
        {
            return rq;
        }

        public void synchronisedBlock()
        {
            string fileName = @"C:\QueueFiles\";
            try
            {
                while (true)
                {
                    Request r = rq.Take();
                    se.WaitOne();
                    using (StreamWriter sw = new StreamWriter(fileName + r.Id + r.user_Id + ".txt"))
                    {
                        sw.Write(r.Id + " " + r.message + " " + r.user_Id + "hellu");
                    }
                    Thread.Sleep(4000);
                    se.Release();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                se.Release();
            }
        }
    }
}