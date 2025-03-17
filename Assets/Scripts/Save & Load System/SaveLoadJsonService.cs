using Interfaces;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace SAVELOAD
{
    public class SaveLoadJsonService : ISaveLoadService
    {
        public void Save(string key, object data)
        {
            var path = "Assets/[TEST]" + "/" + key + ".json";
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }

        public void Load<T>(string key, out T data)
        {
            var path = "Assets/[TEST]" + "/" + key + ".json";
            var json = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<T>(json);
        }
    }
}