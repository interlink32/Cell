using Connection;
using Dna;
using Dna.profile;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    abstract class myservice<T> : service<T> where T : question
    {

    }
}