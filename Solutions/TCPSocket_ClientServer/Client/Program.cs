using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientServer
{
    public class Client
    {
        public static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("localhost", 5002);
                Console.WriteLine("connected to server");

                NetworkStream stream = client.GetStream();

                string msg = "Hello Server!";
                byte[] data = Encoding.UTF8.GetBytes(msg);
                stream.Write(data, 0, data.Length);


                byte[] buffer = new byte[1024];
                int byteRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, byteRead);
                Console.WriteLine("Server Say: " + response);

                Console.WriteLine("!!!!!...........Start Conversation.............!!!!!  ");

             
                string m;
                do
                {
                      m= Console.ReadLine().ToString();
                      byte[] clientmsg=Encoding.UTF8.GetBytes(m);
                      stream.Write(clientmsg, 0, clientmsg.Length);
                    
                    
                    byte[] buff = new byte[1024];
                    int byteR = stream.Read(buff, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buff, 0, byteR);
                    Console.WriteLine("\nServer Say's: " + message);

                } while (m!="bye");

                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error::  "+ e.Message);
            }
        }
    }
}