﻿using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public abstract class service_gene
    {
        internal service service;
        internal service_gene() { }
        internal abstract Task<answer> z_get_answer(question request);
        internal abstract string z_gene { get; }
    }
    public abstract class service_gene<T> : service_gene where T : question
    {
        internal sealed override string z_gene { get; }
        public service_gene()
        {
            z_gene = typeof(T).Name;
        }
        public async Task<bool> notify(notify notify)
        {
            return await service.send_notify(notify);
        }
        public abstract Task<answer> get_answer(T request);
        internal sealed override Task<answer> z_get_answer(question request)
        {
            return get_answer(request as T);
        }
    }
}