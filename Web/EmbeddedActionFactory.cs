using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Microsoft.Extensions.FileProviders;

namespace CodeFactory
{
    public class EmbeddedActionFactory : IEnemyActions, IDisposable
    {
        private static readonly int CardHeight = 179;
        private static readonly int CardWidth = 276;
        private readonly IFileProvider _fileProvider;
        private bool disposed = false;
        private Lazy<IEnumerable<Bitmap>> _actions;

        public IEnumerable<Bitmap> Actions => _actions.Value;

        public EmbeddedActionFactory(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider ?? throw new ArgumentNullException(nameof(fileProvider));
            _actions = new Lazy<IEnumerable<Bitmap>>(LoadActions);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_actions.IsValueCreated)
                    {
                        foreach (var image in _actions.Value)
                        {
                            image.Dispose();
                        }

                    }
                }
                disposed = true;
            }
        }

        private IEnumerable<Bitmap> LoadActions()
        {
            var actions = new List<Bitmap>();
            var contents = _fileProvider.GetDirectoryContents(".").Where(i => i.Name.Contains("actionsmatrix", StringComparison.OrdinalIgnoreCase));
            foreach (var file in contents)
            {
                var matrix = new Bitmap(file.CreateReadStream());
                for (int row = 0; row < 3; row++)
                {
                    for (int column = 0; column < 6; column++)
                    {
                        actions.Add(matrix.Clone(new Rectangle(CardWidth * column, CardHeight * row, CardWidth, CardHeight), PixelFormat.DontCare));
                    }
                }
                matrix.Dispose();
            }
            return actions;
        }
    }
}
