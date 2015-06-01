package net.lockan.heylisten;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.View;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;


public class HeyListenMain extends Activity {



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_hey_listen_main);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_hey_listen_main, menu);
        return true;
    }

    public void sendHeyListen(View v) {
        UDPBroadcastTask heylistentask = new UDPBroadcastTask();
        heylistentask.execute();
    }
}
