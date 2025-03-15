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
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(Application.dataPath + "/" + key + ".json", json);
        }

        public void Load<T>(string key, out T data)
        {
            var json = File.ReadAllText(Application.dataPath + "/" + key + ".json");
            data = JsonConvert.DeserializeObject<T>(json);
        }
    }
}