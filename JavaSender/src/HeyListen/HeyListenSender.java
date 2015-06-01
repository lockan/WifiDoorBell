package HeyListen;

import java.util.Scanner;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

public class HeyListenSender {
	
	private static final int PORT = 9050;
	private static final int PACKETSIZE = 64;
	private static final String HOSTNAME= "192.168.1.255";
	

	public static void main(String[] args) {
		
		System.out.println("Running HeyListen...");
		InetAddress host;
		DatagramSocket socket;
		DatagramPacket packet;
		
		try
		    {
				byte[] payload = new byte[PACKETSIZE];
				String sendstring = "Hey, Listen!";
				payload = sendstring.getBytes();
		        
				host = InetAddress.getByName(HOSTNAME);
		        socket = new DatagramSocket (null);
		        packet = new DatagramPacket (payload, payload.length, host, PORT);
		        System.out.println("Tring to send UDP packet...");
		        socket.send (packet);
		        socket.close ();
		        System.out.println("Done sending.");
		    }
		    catch(Exception ex)
		    {
		        ex.printStackTrace();
		    }
		}
}
