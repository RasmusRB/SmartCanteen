using System;

namespace UdpProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            ProxyWorker worker = new ProxyWorker();
            worker.Start();
        }
    }
}
