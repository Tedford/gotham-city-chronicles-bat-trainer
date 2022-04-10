using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFactory
{
    public class Action
    {
        public IEnumerable<Activation> Activation { get; set; }
        public bool Dredge { get; set; }
        public Event Event { get; set; }
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
    }
}
