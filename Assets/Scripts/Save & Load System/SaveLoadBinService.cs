using System.IO;
using Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace SAVELOAD
{
    public class SaveLoadBinService: ISaveLoadService
    {
        public void Save(string key, object data)
        {
            //var json = JsonConvert.SerializeObject(data);
            //File.WriteAllText(Application.dataPath + "/" + key + ".json", json);
        }

        public void Load<T>(string key, out T data)
        {
            var bin = File.ReadAllText(Application.dataPath + "/" + key + ".json");
            data = JsonConvert.DeserializeObject<T>(bin);
        }
    }
}