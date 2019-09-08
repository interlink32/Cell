using Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class s
    {
        static IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
        internal static void save(string key, object o)
        {
            if (o == null)
                storage.DeleteFile(key);
            else
            {
                var value = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(o));
                value = core.Combine(BitConverter.GetBytes(value.Length), value);
                write(key, value);
            }
        }
        private static void write(string key, byte[] value)
        {
            using (var dv = storage.OpenFile(key, System.IO.FileMode.OpenOrCreate))
            {
                dv.Write(value, 0, value.Length);
                dv.Close();
            }
        }
        public static async Task<T> load<T>(string key)
        {
            using (var dv = storage.OpenFile(key, System.IO.FileMode.OpenOrCreate))
            {
                byte[] data = new byte[4];
                await dv.ReadAsync(data, 0, data.Length);
                data = new byte[BitConverter.ToInt32(data, 0)];
                if (data.Length == 0)
                    return default;
                await dv.ReadAsync(data, 0, data.Length);
                var str = Encoding.UTF8.GetString(data);
                return JsonConvert.DeserializeObject<T>(str);
            }
        }
    }
}
