using Dna;
using stemcell;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace servercell
{
    public abstract class listener
    {
        TcpListener tcp;
        public listener(IPEndPoint endPoint)
        {
            tcp = new TcpListener(endPoint);
            tcp.Start();
            running();
        }
        async void running()
        {
            var dv = await tcp.AcceptTcpClientAsync();
            add(dv);
            running();
        }
        async void add(TcpClient tcp)
        {
            timeout timeout = new timeout(5000, () =>
              {
                  tcp.GetStream().Close();
                  tcp.Close();
              });
            timeout.start();
            byte[] data = new byte[2];
            await tcp.GetStream().ReadAsync(data, 0, data.Length);
            byte clienttype = data[0];
            data = new byte[data[1]];
            await tcp.GetStream().ReadAsync(data, 0, data.Length);
            timeout.end();
            var service = getservice(clienttype);
            if (service.login(data))
            {
                tcp.GetStream().WriteByte((byte)byteid.login);
                service.tcp = tcp;
                service.start();
            }
            else
            {
                tcp.GetStream().WriteByte((byte)byteid.invalid);
                await Task.Delay(5000);
                tcp.GetStream().Close();
                tcp.Close();
            }
        }
        protected abstract servicebase getservice(byte clienttype);
    }
}