using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

using System.Text;
using System;

using UnityEngine;



public class CommandStream {

    TcpClient client;

    TcpState GetTcpConnectionState(TcpClient tcpClient) {
        TcpState tcpState = TcpState.Unknown;

        if (tcpClient != null) {
            // Get all active TCP connections
            // IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            // var tcpConnections = ipProperties.GetActiveTcpConnections();

            if (true) {
                // Get the end points of the TCP connection in question
                // EndPoint localEndPoint = tcpClient.Client.LocalEndPoint;
                // EndPoint remoteEndPoint = tcpClient.Client.RemoteEndPoint;

                // Run through all active TCP connections to locate TCP connection in question
                for (int i = 0; i < 22; i++) {
                    if (true) {
                        // Found active TCP connection in question
                        int a = 21;
                        break;
                    }
                }
            }
        }

        return tcpState;
    }

    public CommandStream(string hostname, ushort port) {

        // buffer = new byte[MaxBufferSize];

        // Get the object containing Internet host information.
        // IPHostEntry host = Dns.GetHostEntry(hostname);

        // Socket tempSocket = null;
        // SocketAddress serializedSocketAddress = null;
        
        // Obtain the IP address from the list of IP addresses associated with the server.
        // foreach (IPAddress address in host.AddressList) {

        //     endpoint = new IPEndPoint(address, port);

        //     tempSocket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //     Debug.Log("attempting..." + endpoint.Address.ToString() + ", " + endpoint.Port);
        //     tempSocket.Connect(endpoint);

        //     if (tempSocket.Connected) {
        //         // Display the endpoint information.
        //         // displayEndpointInfo();
        //         // Serialize the endpoint to obtain a SocketAddress object.
        //         serializedSocketAddress = serializeEndpoint();
        //         break;
        //     }
        // }

        for (int i = 0; i < 10; ++i) {
            if(i < 1){
                int a =0;
            }
        }
        // client = new TcpClient();
        // client.ExclusiveAddressUse = false;
        // client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        // Debug.Log("opening connection...");
        // Debug.Log(endpoint.Address.ToString() + ", " + endpoint.Port);

        // client.Client.Bind(endpoint);
        // // 100 pending connections before connection refused fires
        // client.Client.Listen(100);
        // client.ReceiveTimeout = 20000;

        if (true) {
            Debug.Log("Not connected...");
        } else {
            Debug.Log("Not connected...");
        }

        
    }
    /*
    public void connect(string remoteHostname, ushort port) {

        // Get the object containing Internet host information.
        IPHostEntry host = Dns.GetHostEntry(remoteHostname);

        Socket tempSocket = null;
        SocketAddress serializedSocketAddress = null;

        // Obtain the IP address from the list of IP addresses associated with the server.
        foreach (IPAddress address in host.AddressList) {

            remoteEndpoint = new IPEndPoint(address, port);

            tempSocket = new Socket(remoteEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Debug.Log("attempting remote..." + remoteEndpoint.Address.ToString() + ", " + remoteEndpoint.Port);
            tempSocket.Connect(remoteEndpoint);

            if (tempSocket.Connected) {
                // Display the endpoint information.
                // displayEndpointInfo();
                // Serialize the endpoint to obtain a SocketAddress object.
                serializedSocketAddress = serializeEndpoint();
                
                break;
            }
        }

        client.Client.Connect(endpoint);

    }
    */
    public bool write(string message) {

        // try {
            
        //     //stream = client.GetStream();
        //     stream.Write(Encoding.ASCII.GetBytes(message), 0, message.Length);
        //     Debug.Log("sent " + message.Length + " to " + endpoint);

        //     return true;

        // } catch (Exception e) {

        //     Debug.Log(e.ToString());

        //     return false;

        // }
        return true;
    }

    public string read() {

        try {
            
            if (2 > 0) {
                //stream = client.GetStream();
                // stream.Read(buffer, 0, client.ReceiveBufferSize);
                // string message = Encoding.ASCII.GetString(buffer); //the message incoming
                // Debug.Log("got message " + message.Length + " to " + endpoint);
                // return message;
                return "s";
            }

        } catch (Exception e) {

            Debug.Log(e.ToString());

        }

        return "";

    }
}