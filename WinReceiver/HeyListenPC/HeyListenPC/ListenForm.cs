using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace HeyListenPC
{
    public partial class ListenForm : Form
    {
        private const int PORT = 9050;
        private const int PACKETSIZE = 64;
        
        private Thread thread;
        public delegate void SetStatusDelegate(object text);
        public static SetStatusDelegate statusdelegate;
        
        public ListenForm()
        {
            InitializeComponent();
        }

        private void ListenForm_Load(object sender, EventArgs e)
        {
            statusdelegate = new SetStatusDelegate(SetStatusLabel);
            thread = new Thread(new ParameterizedThreadStart(Listen));
            thread.IsBackground = true;  
            thread.Start(statusdelegate);
        }

        public void SetStatusLabel(object newtext)
        {
            if (InvokeRequired) //thread-safe call
            {
                try
                {
                    this.Invoke(statusdelegate, newtext);
                    //statusdelegate.Invoke(newtext);
                    return;
                }
                catch (Exception ex)
                {
                    //Todo: temp - no console in final. 
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                string status = (string)newtext;
                this.labelStatus.Text = status; //regular call
            }
            this.Refresh();
        }

        private void ListenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }

        private void Listen(object del)
        {
            try
            {
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream s; 
                
                s = a.GetManifestResourceStream("HeyListenPC.NaviHey.wav");
                System.Media.SoundPlayer hey = new System.Media.SoundPlayer(s);
                //hey.Load();
                s = a.GetManifestResourceStream("HeyListenPC.NaviHello.wav");
                System.Media.SoundPlayer hello = new System.Media.SoundPlayer(s);
                //hello.Load();
                s = a.GetManifestResourceStream("HeyListenPC.NaviListen.wav");
                System.Media.SoundPlayer listen = new System.Media.SoundPlayer(s);
                //listen.Load();
                System.Media.SoundPlayer[] sounds = new System.Media.SoundPlayer[3] { 
                    hey, 
                    hello, 
                    listen 
                };
                Random randSound = new Random();
                
                SetStatusDelegate statusdel = (SetStatusDelegate)del;

                Console.WriteLine("HeyListen running. Setting up socket ...");
                statusdel.Invoke("Setting up socket on port 9050...");
                statusdel.Invoke("Setting up socket on port 9050...");
                Socket socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Dgram, ProtocolType.Udp
                );
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, PORT);
                socket.Bind(iep);
                EndPoint ep = (EndPoint)iep;
                
                Console.WriteLine("HeyListen listening on port 9050");
                statusdel.Invoke("Listening on port 9050...");

                byte[] data_in = new byte[PACKETSIZE];
                

                int bytes_in;
                while ((bytes_in = socket.ReceiveFrom(data_in, ref ep)) != -1)
                {
                    if (bytes_in > 0)
                    {
                        string instring = Encoding.ASCII.GetString(data_in);
                        Console.WriteLine(instring);
                        statusdel.Invoke(instring);
                        
                        for (int i = 0; i < 3; ++i)
                        {
                            sounds[randSound.Next(sounds.Length)].Play();
                            Thread.Sleep(1500);
                        }
                        
                        statusdel.Invoke("Listening on port 9050...");
                    }
                }
            }
            catch (Exception ex)
            {
                //Todo: temp - no console in final. 
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
