﻿using stemcell;
using Dna;
using Dna.common;
using servercell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messageserver
{
    class q2loadmessage : service<q_loaddiff>
    {
        public async override Task<answer> getanswer(q_loaddiff question)
        {
            return await db.loaddiff(question.z_user, question.index);
        }
    }
}