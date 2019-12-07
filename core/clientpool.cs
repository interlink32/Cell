using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public abstract class clientpool
    {
        protected async static Task<byte[]> send(IPEndPoint endPoint, long user, byte[] data)
        {
            await Task.CompletedTask;
            return null;
        }
        protected abstract Task<byte[]> getlogininfo(long user);
        protected abstract Task<bool> checklogin(byte[] answer);
    }
}