

using Microsoft.AspNetCore.SignalR.Client;

namespace view
{
    class Program
    {      
        

        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/messageHub";


            await using var connection = new HubConnectionBuilder().WithUrl(url).Build();

            await  connection.StartAsync();

            CopyTraders cp = new CopyTraders(connection);
            cp.MonitorarAsync();
         //  Console.WriteLine(CopyTraders.ReturnNome());
        }

    }


}
