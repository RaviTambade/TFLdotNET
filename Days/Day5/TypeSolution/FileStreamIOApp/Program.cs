using System.IO;
using System.Text;

//Logic to write data to file using FileStream class

string  data="Learn to do ordinary things extraordinarily well";
byte[] bytes=Encoding.UTF8.GetBytes(data);

FileStream fileWriteStream=new FileStream("transflower.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
fileWriteStream.Write(bytes,0,bytes.Length);
Console.WriteLine("Data written to file successfully!");
fileWriteStream.Close();

//Logic to read data from file
string path="transflower.txt";
byte[] readBytes=new byte[100];

FileStream fileReadStream=new FileStream(path, FileMode.Open, FileAccess.Read);
fileReadStream.Read(readBytes,0,readBytes.Length);
string readData=Encoding.UTF8.GetString(readBytes);
Console.WriteLine("Data read from file: "+readData);
fileReadStream.Close();

