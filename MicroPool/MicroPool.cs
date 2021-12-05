using System.Collections.Concurrent;

namespace MicroPool
{
    internal sealed class MicroPool<T> where T: IPoolItem, new()
    {
        private readonly int _maxCapacity;
        private int _capacity;
        private readonly ConcurrentQueue<T> _pool;

        public MicroPool(int maxCapacity)
        {
            _pool = new ConcurrentQueue<T>();
            _maxCapacity = maxCapacity;
        }

        public T Get()
        {
            if (_capacity + 1 > _maxCapacity) throw new ArgumentOutOfRangeException();

            _capacity++;

            if (_pool.Count > 0)
            {
                var res = _pool.TryDequeue(out T item);
                if (!res) return new T();

                return item;
            }

            return new T();
        }

        public void Release(T poolItem)
        {
            if(poolItem == null) throw new ArgumentNullException(nameof(poolItem));

            poolItem.Reset();

            _pool.Enqueue(poolItem);
        }
    }
}
