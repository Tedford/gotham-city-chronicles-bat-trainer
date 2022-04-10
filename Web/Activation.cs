using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFactory
{
    public class Activation
    {
        public int? Tile { get; set; } = new int();
        public bool ActivateLeaders { get; set; }
        public IEnumerable<Objectives> Objective { get; set; }
    }
}
