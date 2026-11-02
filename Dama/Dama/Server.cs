using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Dama
{
    public class Server
    {
        // Lista delle socket dei client attivi (massimo 2)
        public static List<Socket> activeClients = new List<Socket>();

        // ABBIAMO CAMBIATO IL NOME DA 'Main' A 'Avvia' E TOLTO GLI ARGS
        public static void Avvia()
        {
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Nota: Console.WriteLine scriverà nella console di debug di Visual Studio 
            // mentre la tua Form1 è in primo piano
            Console.WriteLine("Server avviato sulla porta 5000. In attesa di massimo 2 client...");

            while (true)
            {
                Socket clientSocket = listener.Accept();

                lock (activeClients)
                {
                    if (activeClients.Count < 2)
                    {
                        activeClients.Add(clientSocket);
                        Console.WriteLine($"[+] Client connesso. Giocatori attivi: {activeClients.Count}/2");

                        // Avvia il thread per gestire questo client
                        Thread t = new Thread(() => HandleClient(clientSocket));
                        t.Start();
                    }
                    else
                    {
                        // Rifiuta il 3° client
                        Console.WriteLine("[-] Connessione rifiutata: stanza piena.");
                        byte[] errorMsg = Encoding.ASCII.GetBytes("ERROR: Stanza piena$");
                        clientSocket.Send(errorMsg);
                        clientSocket.Close();
                    }
                }
            }
        }

        public static void HandleClient(Socket clientSocket)
        {
            byte[] bytes = new byte[1024];

            try
            {
                while (true)
                {
                    string data = "";

                    // Ricezione basata sul delimitatore "$"
                    do
                    {
                        int bytesRec = clientSocket.Receive(bytes);
                        if (bytesRec == 0) break;
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    } while (data.IndexOf("$") == -1);

                    Console.WriteLine($"[RICEVUTO] {data}");

                    // Inoltra il pacchetto direttamente all'altro client (routing)
                    lock (activeClients)
                    {
                        foreach (var c in activeClients)
                        {
                            if (c != clientSocket)
                            {
                                c.Send(Encoding.ASCII.GetBytes(data));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Gestione base in caso di interruzione forzata
            }
            finally
            {
                lock (activeClients)
                {
                    activeClients.Remove(clientSocket);
                    Console.WriteLine($"[-] Client disconnesso. Giocatori attivi rimasti: {activeClients.Count}/2");
                }
                clientSocket.Close();
            }
        }
    }
}
