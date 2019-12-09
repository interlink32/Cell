﻿using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using core;
using Dna;
using Dna.common;
using Dna.user;
using stemcell;

namespace servercell
{
    internal class responder : tcpserver
    {
        private mainserver mainserver;
        private Func<question, Task<answer>> getanswerf;
        public long userid { get; private set; }
        public responder(mainserver mainserver, TcpClient tcp, byte[] privatekey, Func<question, Task<answer>> getanswer) : base(tcp, privatekey)
        {
            this.mainserver = mainserver;
            this.getanswerf = getanswer;
        }
        protected override void close()
        {
            mainserver.remove(this);
        }
        protected override async Task<byte[]> getanswer(byte[] data)
        {
            var gene = converter.change(data) as question;
            answer answer = await answer2(gene);
            data = converter.change(answer);
            return data;
        }
        async Task<answer> answer2(question question)
        {
            switch (question.z_permission)
            {
                case e_permission.free:
                    {
                        if (question is q_login q_login)
                            return await login(q_login);
                        else
                            return await getanswerf(question);
                    }
                case e_permission.user:
                    {
                        if (userid == 0)
                            return error.create("blfnkgmbknfkbmmbmfmb");
                        else
                        {
                            question.z_user = userid;
                            return await getanswerf.Invoke(question);
                        }
                    }
                case e_permission.server:
                    {
                        throw new Exception("gfkbkdkbmf");
                    }
                default:
                    {
                        throw new Exception("dkmrkkfmdmbmc");
                    }
            }
        }

        private async Task<answer> login(q_login question)
        {
            if (mainserver.chromosome == e_chromosome.user)
            {
                var dv = await getanswerf(question);
                if (dv is q_login.done done)
                {
                    if (userid == 0)
                    {
                        userid = done.userid;
                        if (question.notifier)
                            mainserver.add(this);
                    }
                }
                return dv;
            }
            else
            {
                if (userid != 0)
                {
                    Console.Beep();
                    throw new Exception("lblflblgbkfmnmfnkfknkfkb");
                }
                var dv = await client.question(0, question);
                if (dv is q_login.done done)
                {
                    userid = done.userid;
                    if (question.notifier)
                        mainserver.add(this);
                }
                return dv;
            }
        }
    }
}