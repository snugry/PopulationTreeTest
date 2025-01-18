// See https://aka.ms/new-console-template for more information
using PopulationTreeTest;
using PopulationTreeTest.FileIO;

Console.WriteLine("Hello, World!");

Timeline timeline = new Timeline(25, -10000);

timeline.AddDisaster(1352, 500, "The Great Fire");
timeline.AddDisaster(1933, 1000, "The Great War");
timeline.AddDisaster(2084, 2000, "The Comet");
timeline.CalculateTimeline(-10000, 4000);

Random rand = new Random();

//GraphWriter graph = new();
//var randComm = timeline.GetCommunitiesFromYear(rand.Next(1800, 3000)).First();

//graph.WriteGraph(randComm);

for(int i = -10000; i < 4001; i+= 500)
{
    var persons = timeline.GetPersonsFromYear(i);
    var communities = timeline.GetCommunitiesFromYear(i);

    int personsYoung = persons.Where(p => p.GetAge(i) < 60 && p.GetAge(i) > 18).Count();
    int personsWChildren = persons.Where(p => p.Children != null && p.Children.Count() > 0).Count();

    Console.WriteLine($"{i}: {persons.Count} persons, {personsYoung} young persons, {personsWChildren} person have children, {communities.Count} communities");
    Console.WriteLine("Random person:");
    var p = persons[rand.Next(0, persons.Count)];
    Console.WriteLine($"Hi, I'm {p.Prename} {p.Surname} - born on {p.BirthDate}. My Job is {p.Job}");
    if(p.Partner != null)
    {
        Console.WriteLine($"My Partner is {p.Partner.Prename} {p.Partner.Surname}");
    }
    if(p.Parents != null)
    {
        Console.WriteLine($"My Parents are {p.Parents[0].Prename} {p.Parents[0].Surname} & {p.Parents[1].Prename} {p.Parents[1].Surname}");
    }
    else
    {
        Console.WriteLine($"I don't know who my parents are");
    }
    if(p.Children != null && p.Children.Count > 0)
    {
        Console.WriteLine($"I have {p.Children.Count} children");
        if(p.Family.Children != null)
        {
            Console.WriteLine($"My family has {p.Family.Children.Count} children");
        }          
    }
    if(p.Family != null && p.Family.Home != null)
    {
        Console.WriteLine($"I live in house number: {p.Family.Home.House.Id} on the {p.Family.Home.Floor} floor");
    }
}

// Test Remove persons
var personsTest = timeline.GetPersonsFromYear(1999);

var pTest = personsTest[rand.Next(0, personsTest.Count)];
timeline.RemovePersonAndAncestors(pTest, 1999);

personsTest = timeline.GetPersonsFromYear(2000);
var communitiesTest = timeline.GetCommunitiesFromYear(2000);
Console.WriteLine($"2000: {personsTest.Count} persons, {communitiesTest.Count} communities");

// TODO: Recalculate communities when a person was removed!
timeline.CalculateTimeline(2000, 4000, false);

personsTest = timeline.GetPersonsFromYear(4000);
communitiesTest = timeline.GetCommunitiesFromYear(4000);
Console.WriteLine($"4000: {personsTest.Count} persons, {communitiesTest.Count} communities");


