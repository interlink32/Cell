using Dna.user;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class s
    {
        static IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
        internal async static void save(string key, object o)
        {
            await locking.WaitAsync();
            if (o == null)
                write(key, new byte[0]);
            else
            {
                var value = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(o));
                value = core.Combine(BitConverter.GetBytes(value.Length), value);
                write(key, value);
            }
            locking.Release();
        }
        static void write(string key, byte[] value)
        {
            var dv =  get(key);
            dv.Write(value, 0, value.Length);
        }

        public static async Task<T> load<T>(string key)
        {
            await locking.WaitAsync();
            var dv =  get(key);
            byte[] data = new byte[4];
            dv.Read(data, 0, data.Length);
            data = new byte[BitConverter.ToInt32(data, 0)];
            if (data.Length == 0)
            {
                locking.Release();
                return default;
            }
            dv.Read(data, 0, data.Length);
            locking.Release();
            var str = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(str);
        }
        static SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        static Dictionary<string, Stream> dic = new Dictionary<string, Stream>();
        private static Stream get(string key)
        {
            Stream dv = null;
            if (!dic.TryGetValue(key, out dv))
            {
                dv = storage.OpenFile(key, FileMode.OpenOrCreate);
                dic.Add(key, dv);
            }
            dv.Position = 0;
            return dv;
        }
    }
}