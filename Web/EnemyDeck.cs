using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFactory
{
    public class EnemyDeck
    {
        private readonly IVirtualOverlord _actions;

        public EnemyDeck(IVirtualOverlord actions)
        {
            _actions = actions ?? throw new ArgumentNullException(nameof(actions));
        }


    }
}
