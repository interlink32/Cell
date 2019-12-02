using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.file
{
    public class s_fileinfo : s_fileid
    {
        public string name { get; set; }
        public long size { get; set; }
        public e_filestate state { get; set; }
    }
}