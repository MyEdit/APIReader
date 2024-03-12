using ReadApi;

PersonLocation person = await APIReader.getPersonLocation(1);
if (person != null)
{
    Console.WriteLine(person.LastSecurityPointTime);
    Console.WriteLine("========================");
    Console.WriteLine();
}

List<PersonLocation> personLocations = await APIReader.getPersonsLocations();
if (personLocations != null)
{
    foreach (PersonLocation personlocation in personLocations)
    {
        Console.WriteLine(personlocation.Id);
        Console.WriteLine(personlocation.PersonCode);
        Console.WriteLine(personlocation.PersonRole);
        Console.WriteLine(personlocation.LastSecurityPointNumber);
        Console.WriteLine(personlocation.LastSecurityPointDirection);
        Console.WriteLine(personlocation.LastSecurityPointTime);
        Console.WriteLine();
    }
}