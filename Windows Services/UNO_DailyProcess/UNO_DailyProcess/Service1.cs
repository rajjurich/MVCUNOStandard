using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UNO_DailyProcess
{
    public partial class Service1 : ServiceBase
    {
        Thread t;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            t = new Thread(StartCommunication);
            t.IsBackground = true;
            t.Start();

        }
        protected override void OnStop()
        {
            t.Abort();

        }

        Dictionary<string, Thread> RunningThreads = new Dictionary<string, Thread>();
        public void StartCommunication()
        {

            while (true)
            {
                try
                {
                





                }
                catch(Exception ex)
                {


                }
                
            }


        }


    }
}
