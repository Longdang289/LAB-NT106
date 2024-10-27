using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace lab3_bai4
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string username;

        public Form1()
        {
            InitializeComponent();
        }

        public void ConnectToServer(string ipAddress, int port)
        {
            client = new TcpClient();
            client.Connect(ipAddress, port);
            stream = client.GetStream();

            // Gửi username đến server ngay khi kết nối
            byte[] usernameBuffer = Encoding.UTF8.GetBytes(username);
            stream.Write(usernameBuffer, 0, usernameBuffer.Length);

            // Tạo luồng mới để nhận tin nhắn từ server
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }


        public void SendMessage(string message)
        {
            if (client == null || !client.Connected) return;

            byte[] buffer = Encoding.UTF8.GetBytes($"{message}");
            stream.Write(buffer, 0, buffer.Length);
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            int byteCount;

            while ((byteCount = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                Log(message); // Hiển thị tin nhắn lên khung chat của client
            }
        }

        // Hàm này dùng để cập nhật giao diện một cách an toàn
        public void Log(string message)
        {
            Invoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText(message + Environment.NewLine);
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                username = textBox1.Text;
                ConnectToServer("127.0.0.1", 5000);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên của bạn");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = richTextBox2.Text;
            SendMessage(message);
            richTextBox2.Clear();
        }
    }
}
