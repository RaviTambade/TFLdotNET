using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5002);
            server.Start();

            Console.WriteLine("Server Started on Port 5002...");
            Console.WriteLine("Waiting for Clients...");

            while (true) // Server always running
            {
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Client Connected!");

                // Create new thread for each client
                Thread clientThread =
                    new Thread(HandleClient);

                clientThread.Start(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Method executed by each thread
    static void HandleClient(object obj)
    {
        TcpClient client = (TcpClient)obj;

        try
        {
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead;

            // First message
            bytesRead = stream.Read(buffer, 0, buffer.Length);

            string message =
                Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine("Client Says: " + message);

            // Reply
            string reply = "Hello Client";
            byte[] data = Encoding.UTF8.GetBytes(reply);

            stream.Write(data, 0, data.Length);

            Console.WriteLine("Conversation Started...");

            string clientMsg = "";

            while (clientMsg.ToLower() != "bye")
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                    break;

                clientMsg =
                    Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Client: " + clientMsg);

                // Server reply
                Console.Write("Server: ");
                string serverMsg = Console.ReadLine();

                byte[] serverData =
                    Encoding.UTF8.GetBytes(serverMsg);

                stream.Write(serverData, 0, serverData.Length);
            }

            Console.WriteLine("Client Disconnected.");

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Client Error: " + ex.Message);
        }
    }
}

