using System;
using System.Linq;
using System.Collections.Generic;    
    
namespace quoridor_webAPI.Data.Models
{
    public class PriorityQueue<TItem> : IEnumerable<TItem>, IEnumerable<KeyValuePair<int, TItem>>
    {
        private readonly SortedDictionary<int, Queue<TItem>> _storage;

        public PriorityQueue() : this(Comparer<int>.Default)
        {

        }

        public PriorityQueue(IComparer<int> comparer)
        {
            _storage = new SortedDictionary<int, Queue<TItem>>(comparer);
        }

        public int Count
        {
            get;
            private set;
        }

        public void Enqueue(int priority, TItem item)
        {
            if (!_storage.TryGetValue(priority, out var queue))
                _storage[priority] = queue = new Queue<TItem>();
            queue.Enqueue(item);

            Count++;
        }

        public TItem Dequeue(out int priority)
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty");

            var queue = _storage.First();
            var item = queue.Value.Dequeue();
            priority = queue.Key;

            if (queue.Value.Count == 0)
                _storage.Remove(queue.Key);

            Count--;
            return item;
        }

        public TItem Dequeue() {
                    if (Count == 0)
                        throw new InvalidOperationException("Queue is empty");

                    var queue = _storage.First();
                    var item = queue.Value.Dequeue();

                    if (queue.Value.Count == 0)
                        _storage.Remove(queue.Key);

                    Count--;
                    return item;
                }

        public void Clear()
        {
            _storage.Clear();
        }

        public bool Contains(int priority, TItem item)
        {
            if (_storage.Keys.Contains(priority))
            {
                return _storage[priority].Contains(item);
            }
            else { return false; }
        }

        public bool Contains(TItem item) {
            foreach (int priority in _storage.Keys)
            {
                if (_storage[priority].Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryGetPriority(TItem item, out int priority) {
            priority = -1;

            foreach (int priorityVal in _storage.Keys)
            {
                if (_storage[priorityVal].Contains(item))
                {

                    priority = priorityVal;
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<KeyValuePair<int, TItem>> GetEnumerator() {
            var items = from pair in _storage
                        from item in pair.Value
                        select new KeyValuePair<int, TItem>(pair.Key, item);

            return items.GetEnumerator();
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            var items = _storage.SelectMany(pair => pair.Value);
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
