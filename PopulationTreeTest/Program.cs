// See https://aka.ms/new-console-template for more information
using PopulationTreeTest;

Console.WriteLine("Hello, World!");

Timeline timeline = new Timeline(20, 0);

timeline.CalculateTimeline(-10000, 3000);

Random rand = new Random();

for(int i = -10000; i < 3001; i+= 1000)
{
    var persons = timeline.GetPersonsFromYear(i);
    var communities = timeline.GetCommunitiesFromYear(i);

    Console.WriteLine($"{i}: {persons.Count} persons, {communities.Count} communities");
    Console.WriteLine("Random person:");
    var p = persons[rand.Next(0, persons.Count)];
    Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}, Job: {p.Job}");
}


