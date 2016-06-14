namespace PathFinder.Trips.WebApi.Patterns
{
    using System.Collections.Generic;
    using System.Linq;

    namespace EightPuzzle.Domain
    {
        public class PriorityQueue<T> : IEnumerable<T>
        {
            private readonly List<T> _items;
            private readonly IComparer<T> _comparer;
            public PriorityQueue() : this(new List<T>(), Comparer<T>.Default)
            {

            }

            public PriorityQueue(IComparer<T> comparer) : this(new List<T>(), comparer)
            {
                
            }

            public PriorityQueue(IEnumerable<T> collection, IComparer<T> comparer)
            {
                _comparer = comparer;

                _items = new List<T>();
                foreach (var item in collection)
                {
                    Insert(item);
                }
            }

            public PriorityQueue(IEnumerable<T> collection) : this(collection, Comparer<T>.Default)
            {

            }

            public int Count
            {
                get
                {
                    return _items.Count;
                }
            }

            public void Insert(T item)
            {
                _items.Add(item);

                int i = _items.Count - 1;
                while (i > 0 && _comparer.Compare(_items[i >> 1], _items[i]) > 0)
                {
                    Swap(i, i >> 1);
                    i = i >> 1;
                }
            }

            private void MinHipify(int x)
            {
                int l = x << 1;
                int r = (x << 1) + 1;
                int smallest;
                if (l < _items.Count && _comparer.Compare(_items[l], _items[x]) < 0)
                    smallest = l;
                else
                    smallest = x;
                if (r < _items.Count && _comparer.Compare(_items[r], _items[smallest]) < 0)
                    smallest = r;
                if (smallest != x)
                {
                    Swap(x, smallest);
                    MinHipify(smallest);
                }
            }

            private void Swap(int i, int j)
            {
                if (i == j)
                    return;

                T tmp = _items[i];
                _items[i] = _items[j];
                _items[j] = tmp;
            }

            public T ExtractMin()
            {
                T min = _items.FirstOrDefault();
                if (_items.Count > 0)
                {
                    Swap(0, _items.Count - 1);
                    _items.RemoveAt(_items.Count - 1);
                    MinHipify(0);
                }
                return min;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _items.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }

}
