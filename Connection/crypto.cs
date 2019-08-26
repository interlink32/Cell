using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class crypto
    {
        public static (byte[] public_key, byte[] private_key) create_asymmetrical_keys()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            var pubkey = rsa.ExportCspBlob(false);
            var prikey = rsa.ExportCspBlob(true);
            return (pubkey, prikey);
        }
        public static (byte[] key32, byte[] iv16) create_symmetrical_keys()
        {
            Random random = new Random();
            byte[] key32 = new byte[32];
            byte[] key16 = new byte[16];
            random.NextBytes(key32);
            random.NextBytes(key16);
            return (key32, key16);
        }
        public static async Task<byte[]> Encrypt(byte[] data, byte[] public_key)
        {
            TaskCompletionSource<byte[]> rt = new TaskCompletionSource<byte[]>();
            ThreadPool.QueueUserWorkItem((o) =>
            {
                byte[] encryptedData;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportCspBlob(public_key);
                encryptedData = rsa.Encrypt(data, true);
                rsa.Dispose();
                rt.SetResult(encryptedData);
            });
            return await rt.Task;
        }
        public static async Task<byte[]> Decrypt(byte[] data, byte[] private_key)
        {
            TaskCompletionSource<byte[]> rt = new TaskCompletionSource<byte[]>();
            ThreadPool.QueueUserWorkItem((o) =>
            {
                byte[] decryptedData;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportCspBlob(private_key);
                decryptedData = rsa.Decrypt(data, true);
                rsa.Dispose();
                rt.SetResult(decryptedData);
            });
            return await rt.Task;
        }
        public static async Task<byte[]> Encrypt(byte[] data, byte[] Key, byte[] IV)
        {
            TaskCompletionSource<byte[]> rt = new TaskCompletionSource<byte[]>();
            ThreadPool.QueueUserWorkItem((o) =>
            {
                MemoryStream memoryStream;
                CryptoStream cryptoStream;
                Rijndael rijndael = Rijndael.Create();
                rijndael.Key = Key;
                rijndael.IV = IV;
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.Close();
                rt.SetResult(memoryStream.ToArray());
            });
            return await rt.Task;
        }
        public static async Task<byte[]> Decrypt(byte[] data, byte[] Key, byte[] IV)
        {
            TaskCompletionSource<byte[]> rt = new TaskCompletionSource<byte[]>();
            ThreadPool.QueueUserWorkItem((o) =>
            {
                MemoryStream memoryStream;
                CryptoStream cryptoStream;
                Rijndael rijndael = Rijndael.Create();
                rijndael.Key = Key;
                rijndael.IV = IV;
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.Close();
                rt.SetResult(memoryStream.ToArray());
            });
            return await rt.Task;
        }
    }
}