using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFactory
{
    public interface IEnemyActions
    {
        IEnumerable<Bitmap> Actions { get; }
    }
}
