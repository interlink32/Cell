using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controllibrary
{
    public class database : classp
    {
        public LiteDatabase db;
        string idf = null;
        public override string id
        {
            get { return idf; }
            set
            {
                idf = value;
                db = new LiteDatabase(value);
            }
        }
        public override void dispose()
        {
            db?.Dispose();
        }
    }
}