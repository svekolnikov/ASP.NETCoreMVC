namespace MicroPool
{
    public enum Status : byte
    {
        Free,
        Busy
    }
    internal class PoolItem : IPoolItem
    {
        private static long _id;
        public long Id { get; }
        public string Name { get; private set; }
        public Status Status { get; private set; }

        public PoolItem()
        {
            Id = ++_id;
            Status = Status.Free;   
        }

        public void Load(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(name);

            Name = name;
            Status = Status.Busy;
        }

        public void Reset()
        {
            Status = Status.Free;
            Name = string.Empty;
        }
    }
}
