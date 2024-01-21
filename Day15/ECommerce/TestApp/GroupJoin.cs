
class Language
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Person
{
    public int LanguageId { get; set; }
    public string FirstName { get; set; }
}

static void Sample_GroupJoin_Lambda()
{
    Language[] languages = new Language[]
    {
        new Language {Id = 1, Name = "English"},
        new Language {Id = 2, Name = "Russian"}
    };

    Person[] persons = new Person[]
    {
        new Person { LanguageId = 1, FirstName = "Tom" },
        new Person { LanguageId = 1, FirstName = "Sandy" },
        new Person { LanguageId = 2, FirstName = "Vladimir" },
        new Person { LanguageId = 2, FirstName = "Mikhail" },
    };

    var result = languages.GroupJoin(persons, lang => lang.Id, pers => pers.LanguageId, 
        (lang, ps) => new { Key = lang.Name, Persons = ps });

    Debug.WriteLine("Group-joined list of people speaking either English or Russian:");
    foreach (var language in result)
    {
        Debug.WriteLine(String.Format("Persons speaking {0}:", language.Key));

        foreach (var person in language.Persons)
        {
            Debug.WriteLine(person.FirstName);
        }
    }
}