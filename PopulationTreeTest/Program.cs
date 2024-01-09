// See https://aka.ms/new-console-template for more information
using PopulationTreeTest;

Console.WriteLine("Hello, World!");

/*for(int i = 0; i < 10; i++)
{
    Person p = new Person(nameGenerator, (Gender)(rand.Next(0, 1)));
    p.SetBirthDateRange(0, 30, rand);
    p.SetDeathDateRange(65, rand);

    Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}");
}*/

Timeline timeline = new Timeline(20, 0);

timeline.CalculateTimeline(-10000, 3000);

for(int i = -10000; i < 3001; i+= 1000)
{
    var persons = timeline.GetPersonsFromYear(i);
    var communities = timeline.GetCommunitiesFromYear(i);

    Console.WriteLine($"{i}: {persons.Count} persons, {communities.Count} communities");
}

/*foreach (var p in persons)
{
    Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}");
}*/
