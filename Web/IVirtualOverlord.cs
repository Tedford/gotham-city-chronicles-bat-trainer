using System.Collections.Generic;

namespace CodeFactory
{
    public interface IVirtualOverlord
    {
        IEnumerable<Action> History { get; }

        /// <summary>
        /// Gets the turn identifier.
        /// </summary>
        /// <value>The turn.</value>
        int Turn { get; }

        /// <summary>
        /// Take the next turn.
        /// </summary>
        /// <returns>Action.</returns>
        Action Next();

        /// <summary>
        /// Resets the game to specified seed.
        /// </summary>
        /// <param name="seed">The seed.</param>
        void Reset(int seed = 0);


    }
}
