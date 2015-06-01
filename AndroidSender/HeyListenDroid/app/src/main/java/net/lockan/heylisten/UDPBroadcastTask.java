package net.lockan.heylisten;

import android.os.AsyncTask;
import android.util.Log;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

import java.net.URL;

/**
 * Created by Doktor on 2015-05-31.
 */
public class UDPBroadcastTask extends AsyncTask<Void, Void, Integer> {

    private static final int PORT = 9050;
    private static final int PACKETSIZE = 64;
    private static final String HOSTNAME= "192.168.1.255";

    @Override
    protected Integer doInBackground(Void... params) {
        Log.d("sendHeyListen", "Running HeyListen...");
        InetAddress host;
        DatagramSocket socket;
        DatagramPacket packet;

        try
        {
            byte[] payload = new byte[PACKETSIZE];
            String sendstring = "Hey, Listen!";
            payload = sendstring.getBytes();

            host = InetAddress.getByName(HOSTNAME);
            socket = new DatagramSocket (PORT);
            packet = new DatagramPacket (payload, payload.length, host, PORT);
            packet.setLength(payload.length);
            Log.d("sendHeyListen", "Trying to send UDP packet...");
            socket.send(packet);
            socket.close();

        }
        catch(Exception ex) {
            Log.e("sendHeyListen", ex.getMessage());
            return -1;
        }
        Log.d("sendHeyListen", "Done sending.");
        return 0;
    }
}
