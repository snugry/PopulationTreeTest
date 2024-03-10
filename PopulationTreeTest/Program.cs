// See https://aka.ms/new-console-template for more information
using PopulationTreeTest;

Console.WriteLine("Hello, World!");

Timeline timeline = new Timeline(20, 0);

timeline.CalculateTimeline(-10000, 4000);

Random rand = new Random();

for(int i = -10000; i < 4001; i+= 1000)
{
    var persons = timeline.GetPersonsFromYear(i);
    var communities = timeline.GetCommunitiesFromYear(i);

    Console.WriteLine($"{i}: {persons.Count} persons, {communities.Count} communities");
    Console.WriteLine("Random person:");
    var p = persons[rand.Next(0, persons.Count)];
    Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}, Job: {p.Job}");
    if(p.Partner != null)
    {
        Console.WriteLine($"Partner: {p.Partner.Prename} {p.Partner.Surname}");
    }
}


var personsTest = timeline.GetPersonsFromYear(1999);

var pTest = personsTest[rand.Next(0, personsTest.Count)];
timeline.RemovePersonAndAncestors(pTest);

personsTest = timeline.GetPersonsFromYear(2000);
var communitiesTest = timeline.GetCommunitiesFromYear(2000);
Console.WriteLine($"2000: {personsTest.Count} persons, {communitiesTest.Count} communities");


