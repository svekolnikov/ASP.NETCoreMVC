using System.Collections.Generic;

namespace ThreadWpf
{
    public static class ListExtanions
    {
        public static void AddThreadSafe<T>(this List<T> list, T item)
        {
            lock(list)
                list.Add(item);
        }
    }
}
