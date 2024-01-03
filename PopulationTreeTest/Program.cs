// See https://aka.ms/new-console-template for more information
using PopulationTreeTest;

Console.WriteLine("Hello, World!");

Random rand = new Random(321);
NameGenerator nameGenerator = new NameGenerator(rand);

for(int i = 0; i < 100; i++)
{
    Person p = new Person(nameGenerator, Convert.ToBoolean(rand.Next(0, 1)));

    Console.WriteLine($"{p.Prename} {p.Surname}");
}
