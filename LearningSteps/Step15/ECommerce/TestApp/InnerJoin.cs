static void Sample_Join_Lambda()
{
    string[] warmCountries = { "Turkey", "Italy", "Spain", "Saudi Arabia", "Etiobia" };
    string[] europeanCountries = { "Denmark", "Germany", "Italy", "Portugal", "Spain" };
    var result = warmCountries.Join(
        europeanCountries, warm => warm,
                           european => european, (warm, european) 
                           => warm);
    Debug.WriteLine("Joined countries which are both warm and Europan:");
    foreach (var country in result)
        Debug.WriteLine(country);
}
