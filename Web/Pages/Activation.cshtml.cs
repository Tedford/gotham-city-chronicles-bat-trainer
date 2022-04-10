using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeFactory.Pages
{
    public class ActivationModel : PageModel
    {
        /// <summary>
        /// Gets or sets the activation number for this turn.
        /// </summary>
        /// <value>The number.</value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the tile number being activated.
        /// </summary>
        /// <value>The tile.</value>
        public int Tile { get; set; }

        /// <summary>
        /// Gets the unit URI for the display tile of the unit being activated.
        /// </summary>
        /// <value>The unit URI.</value>
        public string UnitUri => "/images/bane.png";

        public IEnumerable<string> Actions { get; set; } = Enumerable.Empty<string>();

        public void OnGet()
        {
        }
    }
}
