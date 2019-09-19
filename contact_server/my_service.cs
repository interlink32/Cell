using Connection;
using Dna;
using Dna.contact;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    abstract class my_service<T> : service<T> where T : question
    {
        static LiteDatabase db = new LiteDatabase("d://contact.db");
        public LiteCollection<contact> db_contact => db.GetCollection<contact>();
        public state GetState(e_state state)
        {
            switch (state)
            {
                case e_state.none:
                    return contact_server.state.none;
                case e_state.opponent:
                    return contact_server.state.opponent;
                case e_state.defender:
                    return contact_server.state.defender;
                default:
                    throw new Exception();
            }
        }
    }
}