using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private TcpListener listener;
        private Dictionary<TcpClient, string> clients = new Dictionary<TcpClient, string>();
        private bool isRunning = false;

        public Form1()
        {
            InitializeComponent();
        }

        public void StartServer(int port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                isRunning = true;
                AppendTextToChatBox("Server started on port " + port);

                // Bắt đầu luồng lắng nghe client mới
                Thread listenThread = new Thread(() =>
                {
                    while (isRunning)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Thread clientThread = new Thread(() => HandleClient(client));
                        clientThread.IsBackground = true;
                        clientThread.Start();
                    }
                });
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                AppendTextToChatBox("Error starting server: " + ex.Message);
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byteCount;

            try
            {
                // Nhận username từ client ngay khi kết nối
                byteCount = stream.Read(buffer, 0, buffer.Length);
                string username = Encoding.UTF8.GetString(buffer, 0, byteCount).Trim();

                lock (clients) clients[client] = username; // Lưu client với username
                AppendTextToChatBox($"{username} joined the Chat");

                // Thông báo cho tất cả clients về việc client mới tham gia
                string welcomeMessage = $"{username} joined the Chat";
                BroadcastMessage(welcomeMessage);

                // Nhận và phát tin nhắn của client
                while ((byteCount = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    string fullMessage = $"{username}: {message}";
                    AppendTextToChatBox(fullMessage);
                    BroadcastMessage(fullMessage);
                }
            }
            catch
            {
                lock (clients)
                {
                    if (clients.TryGetValue(client, out string username))
                    {
                        clients.Remove(client);
                        AppendTextToChatBox($"{username} left the Chat");
                        BroadcastMessage($"{username} left the Chat");
                    }
                }
                client.Close();
            }
        }

        private void BroadcastMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            lock (clients) // Đồng bộ hóa khi gửi tin nhắn
            {
                foreach (var client in clients.Keys)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    catch
                    {
                        // Bỏ qua client gặp lỗi khi gửi
                    }
                }
            }
        }

        private void AppendTextToChatBox(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendTextToChatBox), message);
            }
            else
            {
                richTextBox1.AppendText(message + Environment.NewLine);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port = 5000;
            StartServer(port);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            listener?.Stop();
            foreach (var client in clients.Keys)
            {
                client?.Close();
            }
        }
    }
}
