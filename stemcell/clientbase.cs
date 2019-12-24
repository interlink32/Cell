using Dna;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace stemcell
{
    abstract class clientbase
    {
        public abstract IPEndPoint endpoint { get; }
        public abstract byte clienttype { get; }
        public abstract byte[] connectiondata { get; }
        public virtual void reconnect() { }
        public TcpClient tcp { get; private set; }
        public bool connect { get; private set; }
        public void disconnect()
        {
            connect = false;
        }
        protected async Task login()
        {
            if (connect)
                return;
            if (tcp != null)
            {
                tcp.GetStream().Close();
                tcp.Close();
            }
            tcp = new TcpClient();
            await tcp.ConnectAsync(endpoint.Address, endpoint.Port);
            var data = connectiondata;
            if (data.Length > 256)
                throw new Exception("lkfjbjfgjnjvbjfjbfjnjvnkbmfjbf");
            await tcp.GetStream().WriteAsync(new byte[] { clienttype, (byte)data.Length }, 0, 2);
            await tcp.GetStream().WriteAsync(data, 0, data.Length);
            data = new byte[1];
            await tcp.GetStream().ReadAsync(data, 0, data.Length);
            if (data[0] != (byte)byteid.login)
                throw new Exception("gjfjbjfjbjfjbjfjbjfhjh");
            connect = true;
            reconnect();
            return;
        }
    }
}