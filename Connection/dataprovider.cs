using Dna;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connection
{
    public class dataprovider<T> where T : dataitem
    {
        LiteDatabase db = default;
        public dataprovider(long user)
        {
            db = new LiteDatabase(new ConnectionString()
            {
                Filename = "dataprovider_" + user,
                Password = "kgkgkdkrobkfkkdmermkgnfnnrm"
            });

        }

    }
}