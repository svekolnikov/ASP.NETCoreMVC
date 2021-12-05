using MicroPool;

Console.WriteLine("Start");

var pool = new MicroPool<PoolItem>(3);

var item_1 = pool.Get();
item_1.Load("item_1");

var item_2 = pool.Get();
item_2.Load("item_2");

var item_3 = pool.Get();
item_3.Load("item_3");

//var item_4 = pool.Get(); // Exception
//item_4.Load("item_4");

Console.WriteLine($"Pool Item Id:{item_1.Id} Name:{item_1.Name} Status: {item_1.Status}");
Console.WriteLine($"Pool Item Id:{item_2.Id} Name:{item_3.Name} Status: {item_2.Status}");
Console.WriteLine($"Pool Item Id:{item_3.Id} Name:{item_3.Name} Status: {item_3.Status}");

item_3.Reset();
Console.WriteLine($"Pool Item Id:{item_3.Id} Name:{item_3.Name} Status: {item_3.Status}");