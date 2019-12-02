using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace controllibrary
{
    public class classprovider<T> where T : classp, new()
    {
        public classprovider()
        {
            callback();
        }
        async void callback()
        {
            await locker.WaitAsync();
            list.RemoveAll(i => DateTime.Now - i.lastused > TimeSpan.FromSeconds(10));
            locker.Release();
            await Task.Delay(1000 * 10);
            callback();
        }
        List<T> list = new List<T>();
        SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        public async Task<T> get(string id)
        {
            await locker.WaitAsync();
            var dv = list.FirstOrDefault(i => i.id == id);
            if (dv == null)
            {
                dv = new T();
                dv.id = id;
                list.Add(dv);
            }
            dv.lastused = DateTime.Now;
            locker.Release();
            return dv;
        }
    }
    public abstract class classp
    {
        public abstract string id { get; set; }
        public abstract void dispose();

        public DateTime lastused = default;
    }
}