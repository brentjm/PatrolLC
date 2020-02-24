using System;
using System.Net;  
using System.Net.Sockets;  
using System.Text;  
using Newtonsoft.Json;
  
public class Request {
    // This class contains parameters necessary for all requests.
    public string user = null;
    public string id = null;
    public string command = null;
    public string method = null;
}

public class PatrolLC {
    
    public PatrolLC() {}
    public string errorMessage = "none";

    public string SelectFunction(string JSONString) {

        Request request = JsonConvert.DeserializeObject<Request>(JSONString);
        Console.WriteLine("Requested command: {0}", request.command);

        switch (request.command)  {
            case "run":
                errorMessage = Run(request.method);
                break;
            default:
                Console.WriteLine( "request {0} not found", request.command);  
                errorMessage = "No such command";
                break;
        }
        return errorMessage;
    }

    public string Run(string method) {
        Console.WriteLine( "Executing run with method {0}", method);  
        // API command here. Need a try/catch.
        return "none";
    }

}


public class SynchronousSocketListener {  
  
    // Incoming data from the client.  
    public static string data = null;  

    public static PatrolLC patrolLC = new PatrolLC();

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
                Socket handler = listener.Accept();  
                data = null;  
  
                // Encode the message as ASCII
                while (true) {  
                    int bytesRec = handler.Receive(bytes);  
                    data += Encoding.ASCII.GetString(bytes,0,bytesRec);  
                    if (data.IndexOf("}") > -1) {  
                        break;  
                    }  
                }  

                Console.WriteLine( "Raw text received : {0}", data);  

                data = patrolLC.SelectFunction(data);
  
                // Send completion message back to client.
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
