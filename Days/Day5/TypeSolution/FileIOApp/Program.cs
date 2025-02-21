//File IO
using System;
using System.IO;

//Static method
string data="Doing ordinary things extra ordinarily";
File.WriteAllText("transflower.txt", data);
string text = File.ReadAllText("transflower.txt");

Console.WriteLine("File is created and read successfully!");
Console.WriteLine("File content: " + text);
