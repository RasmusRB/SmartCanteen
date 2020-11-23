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
        private const string URI = ""; // TODO REST uri
        private readonly UdpClient client = new UdpClient(); // TODO port nr til at modtage UDP datagram
        private byte[] buffer;

        public ProxyWorker()
        {

        }

        internal void Start()
        {
            while (true)
            {
                Kunder obj = ReadUDPPacket();
                SendToRest(obj);
            }
        }

        public Kunder ReadUDPPacket()
        {
            // read udp
            IPEndPoint remotEndPoint = new IPEndPoint(IPAddress.Any, 0);
            buffer = client.Receive(ref remotEndPoint);

            string jsonStr = Encoding.UTF8.GetString(buffer);

            return JsonConvert.DeserializeObject<Kunder>(jsonStr);
        }

        public async void SendToRest(Kunder obj)
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