using System;
using System.Net;  
using System.Net.Sockets;  
using System.Text;  
using Newtonsoft.Json;
  
public class Request {
    /* Class to define parameters used for LC methods. The parameters
     * may be better defined as structures.
     */
    public string user = null;
    public string id = null;
    public string command = null;
    public string method = null;
}

public class PatrolLC {
    /* Class that defines the LC methods. The entry point
     * will be the SelectFunction, which will take the JSONString
     * from the socket server and use a series of swicth-case
     * to call a method that unpacks, and checks the passed
     * arguments and then call the API method.
     *
     * The public
     */
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
    /* Server class binds to a localhost port and listens
     * for a connection. Reads the port until a "}" is received
     * indicating the end of a JSON string. A class member
     * of a PatrolLC object is defined from which SelectFunction
     * is called with the received socket data.
     */
    public static string data = null;  

    public static PatrolLC patrolLC = new PatrolLC();

    public static void StartListening() {  
        byte[] bytes = new Byte[1024];  
  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
        IPAddress ipAddress = ipHostInfo.AddressList[0];  
        // If the server fails, it may be due to the ipHostInfo.AddressList
        // returning the IPV6 address. We want tie IPV4 address, so
        // hard code the address.
        //IPAddress ipAddress = IPAddress.Parse("10.131.1.23");

        Console.WriteLine("Server host IP {0}", ipAddress);
        // Create the endpoint address and port.
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11017);  
  
        Socket listener = new Socket(ipAddress.AddressFamily,  
            SocketType.Stream, ProtocolType.Tcp );  
  
        // Bind the socket to the local endpoint and allow
        // 2 incoming connections. Probably only want one in the
        // final version, but use 2 for debugging purposes.
        try {  
            listener.Bind(localEndPoint);  
            listener.Listen(2);  
  
            // Start listening for connections.  
            while (true) {  
                Console.WriteLine("Waiting for a connection...");  
                // Blocking wait for connection.
                Socket handler = listener.Accept();  
                Console.WriteLine("Connection");  
                data = null;  
  
                // Encode the message as ASCII
                while (true) {  
                    Console.WriteLine("{0}", data);  
                    int bytesRec = handler.Receive(bytes);  
                    data += Encoding.ASCII.GetString(bytes,0,bytesRec);  
                    if (data.IndexOf("}") > -1) {  
                    //if (data.IndexOf("EOF") > -1) {  
                        break;  
                    }  
                }  

                // Write the text to console for debugging.
                Console.WriteLine( "Raw text received : {0}", data);  

                // Call the PatrolLC method with the JSON object.
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
