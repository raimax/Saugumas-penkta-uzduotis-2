using System.Net.Sockets;
using System.Text;

namespace SAUGUMAS_antras
{
    public class Client
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;

        public Client(string server, int port)
        {
            try
            {
                _client = new TcpClient(server, port);
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(Client)}: " + ex.Message);
            }
        }

        public void WaitForResponse()
        {
            try
            {
                // Receive the TcpServer.response.
                byte[] data = new byte[256];
                SerializedData responseData = null;
                int bytes = _stream.Read(data, 0, data.Length);
                responseData = Serializer.Deserialize(data);

                Console.WriteLine("Received from app 2:");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Data: " + Encoding.UTF8.GetString(responseData.OriginalData));
                Console.WriteLine("Signature: " + Convert.ToBase64String(responseData.SignedData));
                Console.ResetColor();

                bool verified = RsaSignature.VerifySignedHash(responseData.OriginalData, responseData.SignedData, responseData.Key);

                if (verified)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine(verified);
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(verified);
                    Console.ResetColor();
                }

                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(WaitForResponse)}: " + ex.Message);
            }

            Console.WriteLine("\n Press Enter key to exit...");
            Console.Read();
        }

        public void SendData(byte[] data)
        {
            try
            {
                // Send the message to the connected TcpServer.
                _stream.Write(data, 0, data.Length);

                Console.WriteLine("Data sent to app 2");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {nameof(SendData)}: " + ex.Message);
            }
        }

        private void Close()
        {
            // Close everything.
            _stream.Close();
            _client.Close();
        }
    }
}
