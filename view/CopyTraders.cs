

using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace view
{
  public class CopyTraders
    {
        private static FileSystemWatcher _monitorar;
       
        private HubConnection connection;
    

        public CopyTraders(HubConnection connection)
        {
            this.connection = connection;
        }

               

        public async Task MonitorarAsync()
        {          


            DirectoryInfo diretorio = new DirectoryInfo("C:\\Program Files(x86)\\Westernpips Private 7\\.logs\r\n");

            _monitorar = new FileSystemWatcher(diretorio.FullName.ToString())
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.CreationTime,
            EnableRaisingEvents = true
            };
            
            _monitorar.Created += OnFileChanged;
           _monitorar.Changed += OnFilalter;
            Console.WriteLine("monitorando");
            Console.ReadKey();

        }

        private async void OnFilalter(object sender, FileSystemEventArgs e)
        {
            string[] readText = null;
            DirectoryInfo diretorio = new DirectoryInfo("C:\\Program Files (x86)\\Westernpips Private 7\\.logs\r\n");

            readText = File.ReadAllLines(diretorio.FullName.ToString() + "\\" + e.Name);

            string s = "null";
            if (readText != null)
            {
                if (readText.Length >= 1)
                {
                    s = readText[readText.Length - 1];
                }
            }

            if (s.Contains("OK") == true || s.Contains("OK") == true)
            {
                return;
            }

            if (s.Contains("SELL") == true || s.Contains("BUY") == true || s.Contains("CLOSE") == true)
            {
                string[] subs = s.Split('>');
                subs[1].Trim();
                string[] substwo = subs[1].Split(' ');

                double Lote, preco;
                var sinal = substwo[1];
                string Symbol = substwo[2].ToString();
                if (sinal == "CLOSE")
                {
                    Lote = 0;
                    preco = 0;
                }
                else
                {
                    Lote = double.Parse(substwo[3], CultureInfo.InvariantCulture);

                    preco =  Convert.ToDouble(substwo[7].Replace(",", ""), CultureInfo.InvariantCulture);
                }                
               

                Sinal sinalsend = new Sinal(sinal,Lote, Symbol, preco);

                var result = JsonConvert.SerializeObject(sinalsend);
                await connection.SendAsync("SendMessage", result);
                Console.WriteLine(result);

            }


            }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($" {DateTime.Now} O Arquivo {e.Name} {e.ChangeType}");
        //    this.inciar();
        }

    }
}
