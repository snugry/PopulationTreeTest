﻿// See https://aka.ms/new-console-template for more information
using PopulationTreeTest;

Console.WriteLine("Hello, World!");

Timeline timeline = new Timeline(25, -10000);

timeline.CalculateTimeline(-10000, 4000);

Random rand = new Random();

for(int i = -10000; i < 4001; i+= 1000)
{
    var persons = timeline.GetPersonsFromYear(i);
    var communities = timeline.GetCommunitiesFromYear(i);

    Console.WriteLine($"{i}: {persons.Count} persons, {communities.Count} communities");
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
}

// Test Remove persons
var personsTest = timeline.GetPersonsFromYear(1999);

var pTest = personsTest[rand.Next(0, personsTest.Count)];
timeline.RemovePersonAndAncestors(pTest, 1999);

personsTest = timeline.GetPersonsFromYear(2000);
var communitiesTest = timeline.GetCommunitiesFromYear(2000);
Console.WriteLine($"2000: {personsTest.Count} persons, {communitiesTest.Count} communities");

// TODO: Recalculate communities when a person was removed!
personsTest = timeline.GetPersonsFromYear(4000);
communitiesTest = timeline.GetCommunitiesFromYear(4000);
Console.WriteLine($"4000: {personsTest.Count} persons, {communitiesTest.Count} communities");


