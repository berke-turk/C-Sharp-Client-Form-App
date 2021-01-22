using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLIENTFORM1
{
    class Client : Form
    {
        public static TcpClient socket = new TcpClient();
        public static NetworkStream stream;
        public static OyunForm oyunForm;
        public static int dataBufferSize = 4096;
        public static byte[] buffer;
        public static void Connect()
        {
            oyunForm = new OyunForm();
            oyunForm.Show();
            socket.BeginConnect(ServerSettings.IPADDRESS, ServerSettings.PORTADDRESS,ConnectCallBack, null);
        }

        public static void Disconnect()
        {
            socket.Close();
        }

        public static void ConnectCallBack(IAsyncResult asyncResult)
        {
            socket.EndConnect(asyncResult);
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            stream = socket.GetStream();
            buffer = new byte[dataBufferSize];
            if (!socket.Connected)
            {
                return;
            }
            stream.BeginRead(buffer, 0, dataBufferSize, ReceiveCallBack, null);
        }

        public static void ReceiveCallBack(IAsyncResult asyncResult)
        {
            try
            {
                int gelenVeriUzunlugu = stream.EndRead(asyncResult);
                if(gelenVeriUzunlugu <= 0)
                {
                    // Disconnect
                    return;
                }

                // Gelen veri bir şey yapılacaksa ki, yapılacak.

                byte[] _data = new byte[gelenVeriUzunlugu];
                Array.Copy(buffer, _data, gelenVeriUzunlugu);

                string json = Encoding.UTF8.GetString(_data);

                Handlers.Handle(json);
                stream.BeginRead(buffer, 0, dataBufferSize, ReceiveCallBack, null);
            }
            catch (Exception)
            {
                // Disconnect
                return;
            }
        }

        public static void POSITION_PICTURE_UPDATE(int _LEFT,int _RIGHT, int _UP, int _DOWN)
        {
            oyunForm.pictureBox1.Left -= _LEFT; // Sola
            oyunForm.pictureBox1.Left += _RIGHT; // Sağa 
            oyunForm.pictureBox1.Top -= _UP; // Yukarı
            oyunForm.pictureBox1.Top += _DOWN; // Aşağı
        }
    }
}
