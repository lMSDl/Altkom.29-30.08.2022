using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Garden
    {
        public int Size { get; }
        private ICollection<string> _items { get; }


        public Garden(int size)
        {
            if (size <= 0)
                throw new ArgumentException();
            Size = size;
            _items = new List<string>();
        }

        public bool Plant(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Roślina musi posiadać nazwę!", nameof(name));
            if (_items.Contains(name))
                throw new ArgumentException("Roślina już istnieje w ogrodzie", nameof(name));

            if (_items.Count() >= Size)
                return false;

            _items.Add(name);
            return true;
        }

        public ICollection<string> GetPlants()
        {
            return _items.ToList();
        }
    }
}
