using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace CodeFactory
{
    public class SimpleVirtualOverlord : IVirtualOverlord, IDisposable
    {
        private static readonly int CardHeight = 179;
        private static readonly int CardWidth = 276;
        private bool disposed = false;
        private Lazy<IEnumerable<Action>> _actions;
        private List<Action> _history = new List<Action>();
        private List<int> _remaining = new List<int>();
        private int _lastSeed;

        public int Turn { get; set; } = 0;

        public IEnumerable<Action> History => _history;

        public SimpleVirtualOverlord()
        {
            _actions = new Lazy<IEnumerable<Action>>(LoadActions);
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

                }
                disposed = true;
            }
        }

        private IEnumerable<Action> LoadActions()
        {
            var asm = Assembly.GetExecutingAssembly();
            using var stream = asm.GetManifestResourceStream(asm.GetManifestResourceNames().First(i => i.Contains("rules-1.5.json")));
            using var reader = new StreamReader(stream);
            return JsonConvert.DeserializeObject<Action[]>(reader.ReadToEnd());
            //foreach (var match in asm.GetManifestResourceNames().Select(i => Regex.Match(i, @"^.+action(?<id>\d+).png", RegexOptions.IgnoreCase)).Where(i => i.Success))
            //{
            //    using var stream = asm.GetManifestResourceStream(match.Value);
            //    var buffer = new byte[stream.Length];
            //    stream.Read(buffer, 0, buffer.Length);
            //    yield return new Action { Id = int.Parse(match.Groups["id"].Value), Image = buffer, Rules = "" };
            //}
        }

        public void Reset(int seed = 0)
        {
            _lastSeed = seed;
            var rand = seed == 0 ? new Random() : new Random(seed);

            int max = _actions.Value.Count();
            _remaining.Clear();
            while (_remaining.Count < max)
            {
                var index = rand.Next(0, max);
                if (!_remaining.Contains(index))
                {
                    _remaining.Add(index);
                }
            }
        }

        public Action Next()
        {
            Turn++;
            if (!_remaining.Any())
            {
                Reset(_lastSeed);
            }
            int index = _remaining.First();
            _remaining.RemoveAt(0);
            var action = _actions.Value.ElementAt(index);
            _history.Add(action);
            return action;
        }
    }
}
