using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Game2048.Infrastructure
{
    public class JsonFileManager
    {
        public static void WriteToJsonFile<T>(string filePath, ObservableCollection<T> players)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(players, options);
            if(!IsFileLocked(filePath))
                File.WriteAllText(filePath, jsonString);
        }
        
        public static ObservableCollection<T> ReadListFromJsonFile<T>(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if(File.Exists(filePath))
            {
                if(!IsFileLocked(filePath))
                    return JsonDeserialize<T>(filePath, options);
                else
                    return new ObservableCollection<T> { };
            }
            else
            {
                File.Create(filePath).Close();
                return new ObservableCollection<T> { };
            }
        }
        
        private static ObservableCollection<T> JsonDeserialize<T>(string filePath, JsonSerializerOptions options)
        {
            string jsonString = File.ReadAllText(filePath);
            try
            {
                return JsonSerializer.Deserialize<ObservableCollection<T>>(jsonString, options);
            }
            catch(JsonException)
            {
                if(jsonString == "")
                {
                    ShowReadErrorMessage();
                    File.WriteAllText(filePath, "");
                }
                return new ObservableCollection<T> { };
            }
        }
        private static bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
                return false;
            }
            catch (IOException)
            {
                return true;
            }
        }


        private static void ShowReadErrorMessage()
        {
            MessageBox.Show("Could not read the statistics file! The statistics will be reset!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
