using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using ModelLib.Model;
using Newtonsoft.Json;

namespace UdpProxy
{
    internal class ProxyWorker
    {
        private const string URI = "http://localhost:63277/api/Customers"; // TODO REST uri der skal postes til
        private readonly UdpClient _client = new UdpClient(7000); // TODO port nr til at modtage UDP datagram
        private byte[] _buffer;

        public ProxyWorker()
        {
        }

        internal void Start()
        {
            while (true)
            {
                Customers obj = ReadUDPPacket();
                SendToRest(obj);
            }
        }

        public Customers ReadUDPPacket()
        {
            // read udp
            IPEndPoint remotEndPoint = new IPEndPoint(IPAddress.Any, 0);
            _buffer = _client.Receive(ref remotEndPoint);

            string count = Encoding.UTF8.GetString(_buffer);

            return new Customers(int.Parse(count), DateTime.Now);
        }

        public async void SendToRest(Customers obj)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                HttpResponseMessage resp = await client.PostAsync(URI, content);

                if (resp.IsSuccessStatusCode)
                {
                    return;
                }
                throw new ArgumentException("Post failed");
            }
        }
    }
}