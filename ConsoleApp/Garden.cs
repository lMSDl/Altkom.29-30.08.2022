using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Garden
    {
        public int Size { get; }
        private ICollection<string> _items { get; }

        private ILogger _logger;

        public Garden(int size, ILogger logger) : this(size)
        {
            _logger = logger;
        }

        public Garden(int size)
        {
            if (size <= 0)
                throw new ArgumentException();
            Size = size;
            _items = new List<string>();
        }

        public bool Plant(string name)
        {

            //_logger?.Log($"Dodawanie rośliny");

            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Roślina musi posiadać nazwę!", nameof(name));
            if (_items.Contains(name))
                throw new ArgumentException("Roślina już istnieje w ogrodzie", nameof(name));

            if (_items.Count() >= Size)
            {
                _logger?.Log($"Brak miejsca na {name}");
                return false;
            }

            _items.Add(name);
            _logger?.Log($"Roślina {name} została dodana do ogrodu");
            return true;
        }

        public ICollection<string> GetPlants()
        {
            return _items.ToList();
        }

        public bool Remove(string name)
        {
            if (!_items.Contains(name))
                return false;

            _items.Remove(name);
            _logger?.Log($"Roślina {name} została usunięta z ogrodu");
            return true;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public int Count()
        {
            return _items.Count;
        }

        public string ShowLastLog()
        {
            return _logger.GetLogs(DateTime.Now.AddMinutes(-1), DateTime.Now).Split('\n').Last();
        }
    }
}
