namespace MicroPool
{
    internal interface IPoolItem
    {
        public long Id { get; }
        public string Name { get; }
        public Status Status { get; }
        public void Load(string name);
        public void Reset();
    }
}
