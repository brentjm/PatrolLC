using System;
using System.Net;  
using System.Net.Sockets;  
using System.Text;  
using Newtonsoft.Json;
  
public class PatrolLC {
    
    public static void SelectFunction(string JSONString) {

        dynamic methodInfo = JsonConvert.DeserializeObject("{ 'method': 'run' }");

        switch (methodInfo)  {
            case "RunMethod":
                // Show the data on the console.  
                Console.WriteLine( "RunMethod method requested, with data {0}", methodInfo);  
                break;
            default:
                Console.WriteLine( "API request not understood (data={0})", methodInfo);  
                break;
        }
  
        //JsonConvert.SerializeObject(JSONString);
        //Console.WriteLine(JSONString);
    }
}


public class SynchronousSocketListener {  
  
    // Incoming data from the client.  
    public static string data = null;  

    public static PatrolLC patrolLC = null;

    public static void StartListening() {  
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];  
  
        // Establish the local endpoint for the socket.  
        // Dns.GetHostName returns the name of the   
        // host running the application.  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
        // Specify the container name instead of using a lookup
        IPAddress ipAddress = ipHostInfo.AddressList[0];  
        //IPAddress ipAddress = IPAddress.Parse("10.131.1.23");
        Console.WriteLine("Server host IP {0}", ipAddress);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11017);  
  
        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,  
            SocketType.Stream, ProtocolType.Tcp );  
  
        // Bind the socket to the local endpoint and   
        // listen for incoming connections.  
        try {  
            listener.Bind(localEndPoint);  
            listener.Listen(10);  
  
            // Start listening for connections.  
            while (true) {  
                Console.WriteLine("Waiting for a connection...");  
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = listener.Accept();  
                data = null;  
  
                // An incoming connection needs to be processed.  
                while (true) {  
                    int bytesRec = handler.Receive(bytes);  
                    data += Encoding.ASCII.GetString(bytes,0,bytesRec);  
                    if (data.IndexOf("<EOF>") > -1) {  
                        break;  
                    }  
                }  

                patrolLC.SelectFunction(data);
  
                // Show the data on the console.  
                Console.WriteLine( "Text received : {0}", data);  

                // Echo the data back to the client.  
                byte[] msg = Encoding.ASCII.GetBytes(data);  
  
                handler.Send(msg);  
                handler.Shutdown(SocketShutdown.Both);  
                handler.Close();  
            }  
  
        } catch (Exception e) {  
            Console.WriteLine(e.ToString());  
        }  
  
        Console.WriteLine("\nPress ENTER to continue...");  
        Console.Read();  
  
    }  
  
    public static int Main(String[] args) {  
        StartListening();  
        return 0;  
    }  
}  