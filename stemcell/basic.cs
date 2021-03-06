﻿using stemcell;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stemcell
{
    public static class basic
    {
        public static Random random = new Random();
        static questioner defaultitem = new questioner(e_chromosome.user.ToString(), 0);
        static async Task<answer> question(question question)
        {
            return await defaultitem.question(question);
        }
        public static void logout(long userid)
        {
            var dv = s.dbuserlogin.FindOne(i => i.id == userid);
            if (dv == null)
                return;
            s.dbuserlogin.Delete(i => i.id == userid);
            client.close(userid);
        }
        public static async Task sendactivecode(string callerid)
        {
            answer rsv = await question(new q_sendactivecode()
            {
                callerid = callerid
            });
            if (!(rsv is q_sendactivecode.done))
                throw new Exception("lkfkbfjbjdjbjkdbjfkvb");
        }
        public static async Task<bool> login(string callerid, string activecode)
        {
            var userlogin = s.dbuserlogin.FindOne(i => i.callerid == callerid);
            if (userlogin != null)
                return true;
            var dv = await question(new q_getusertoken()
            {
                callerid = callerid,
                activecode = activecode
            }) as q_getusertoken.done;
            if (dv == null)
                return false;
            s.dbuserlogin.Upsert(new userlogin()
            {
                id = dv.user,
                callerid = callerid,
                token = dv.token,
                general = true
            });
            return true;
        }
        public static async Task<bool> serverlogin(e_chromosome chromosome, string password)
        {
            var dv = await question(new q_getservertoken()
            {
                chromosome = chromosome,
                password = password
            }) as q_getservertoken.done;
            if (dv == null)
                return false;
            s.dbuserlogin.Upsert(new userlogin()
            {
                token = dv.token,
                general = false,
                id = (int)chromosome,
            });
            return true;
        }
        public static s_chromosome userchromosome = new s_chromosome()
        {
            chromosome = e_chromosome.user,
            endpoint = new IPEndPoint(reference.validip(), 10001).ToString(),
            publickey = resource.user_public_key
        };
        public static async Task<s_chromosome> getchromosome(string chromosome)
        {
            if (chromosome == e_chromosome.user.ToString() || chromosome == e_chromosome.userdata.ToString())
                return userchromosome;
            return (await allchromosome()).First(i => i.chromosome.ToString() == chromosome);
        }
        static s_chromosome[] chromosomes = null;
        static SemaphoreSlim getlock = new SemaphoreSlim(1, 1);
        public static async Task<s_chromosome[]> allchromosome()
        {
            await getlock.WaitAsync();
            if (chromosomes == null)
                chromosomes = (await question(new q_getchromosome()) as q_getchromosome.done).items;
            getlock.Release();
            return chromosomes;
        }
    }
}