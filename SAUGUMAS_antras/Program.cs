namespace SAUGUMAS_antras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 1111);
            server.Start();
        }
    }
}