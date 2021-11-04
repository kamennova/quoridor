using System;
using System.Linq;
using System.Collections.Generic;    
    
namespace quoridor_webAPI.Data.Models
{
    public class PriorityQueue<TPriority, TItem> : IEnumerable<TItem>, IEnumerable<KeyValuePair<TPriority, TItem>>
    {
        private readonly SortedDictionary<TPriority, Queue<TItem>> _storage;

        public PriorityQueue() : this(Comparer<TPriority>.Default)
        {

        }

        public PriorityQueue(IComparer<TPriority> comparer)
        {
            _storage = new SortedDictionary<TPriority, Queue<TItem>>(comparer);
        }

        public int Count
        {
            get;
            private set;
        }

        public void Enqueue(TPriority priority, TItem item)
        {
            if (!_storage.TryGetValue(priority, out var queue))
                _storage[priority] = queue = new Queue<TItem>();
            queue.Enqueue(item);

            Count++;
        }

        public TItem Dequeue()
        {
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

        public bool Contains(TPriority priority, TItem item)
        {
            if (_storage.Keys.Contains(priority))
            {
                return _storage[priority].Contains(item);
            }
            else { return false; }
        }

        public bool Contains(TItem item)
        {
            foreach (TPriority priority in _storage.Keys)
            {
                if (_storage[priority].Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryGetPriority(TItem item, out TPriority priority)
        {
            foreach (TPriority priorityVal in _storage.Keys)
            {
                if (_storage[priorityVal].Contains(item))
                {
                    priority = priorityVal;
                    return true;
                }
            }
            priority = _storage.Keys.Take(1).Last();
            return false;
        }

        public IEnumerator<KeyValuePair<TPriority, TItem>> GetEnumerator()
        {
            var items = from pair in _storage
                        from item in pair.Value
                        select new KeyValuePair<TPriority, TItem>(pair.Key, item);

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