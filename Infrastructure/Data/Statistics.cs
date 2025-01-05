using Game2048.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Infrastructure.Data
{
    internal class Statistics
    {
        public static ObservableCollection<Player> Players { get => JsonFileManager.ReadListFromJsonFile<Player>(GetStatisticFilePath()); }

        public static void Add(string name, string score)
        {
            string jsonPath = GetStatisticFilePath();
            Player player = new Player(name, score);
            ObservableCollection<Player> players = JsonFileManager.ReadListFromJsonFile<Player>(jsonPath);
            if(players.Any(p => p.Name == player.Name))
            {
                Player writtenPlayer = players.FirstOrDefault(p => p.Name == player.Name);
                if (int.Parse(writtenPlayer.Score) < int.Parse(player.Score))
                {
                    writtenPlayer.Score = player.Score;
                }
            }
            else
            {
                players.Add(player);
            }

            JsonFileManager.WriteToJsonFile(jsonPath, players);
        }

        private static string GetStatisticFilePath()
        {
            string pathCurrentUserLocal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            string pathToGameSettingDirectory = Path.Combine(pathCurrentUserLocal, "Game2048");
            if (!Directory.Exists(pathToGameSettingDirectory))
                Directory.CreateDirectory(pathToGameSettingDirectory);

            string pathToJsonFile = Path.Combine(pathToGameSettingDirectory, "statistics.json");
            if (!File.Exists(pathToJsonFile))
                File.Create(pathToJsonFile);

            return pathToJsonFile;
        }
       

        public static void WriteWithSharing(string filePath, string content)
        {
            using (FileStream fs = new FileStream(filePath,
                   FileMode.Create,
                   FileAccess.Write,
                   FileShare.Read))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(content);
            }
        }

    }
}
    