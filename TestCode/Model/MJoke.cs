using System;
using System.Collections.Generic;
using System.Text;

namespace TestCode.Model
{
    public class MJoke
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool? Evaluate { get; set; }
        public bool IsView { get; set; }
    }
}
