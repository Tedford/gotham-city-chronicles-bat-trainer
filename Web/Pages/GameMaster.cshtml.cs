using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeFactory.Pages
{
    public class GameMasterModel : PageModel
    {
        private readonly IVirtualOverlord _overlord;

        public Uri CurrentActionUri { get; private set; }

        public Action Current { get; private set; }

        public int Turn => _overlord.Turn;

        public IEnumerable<Action> History => _overlord.History;

        public GameMasterModel(IVirtualOverlord actions)
        {
            _overlord = actions ?? throw new ArgumentNullException(nameof(Action));
        }

        public IActionResult OnGet()
        {
            DetermineNextAction();
            return Page();
        }

        public Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(Page());
            }
            DetermineNextAction();
            return Task.FromResult<IActionResult>(Page());
        }

        private void DetermineNextAction()
        {
            Current = _overlord.Next();
            CurrentActionUri = new Uri($"data:image/png;base64,{Convert.ToBase64String(Current.Image)}");
        }
    }
}
