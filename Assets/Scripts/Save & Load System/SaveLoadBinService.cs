using System.IO;
using Newtonsoft.Json;
using Interfaces;
using MessagePack;
using UnityEngine;

namespace SAVELOAD
{
    public class SaveLoadBinService: ISaveLoadService
    {
        public void Save(string key, object data)
        {
            byte[] bytes = MessagePackSerializer.Serialize(data);
            string filePath = "Assets/[TEST]" + "/" + key + ".msgpack";
            
            File.WriteAllBytes(filePath, bytes);
            Debug.Log($"Данные сохранены по ключу '{key}' в файл: {filePath}");
        }

        public void Load<T>(string key, out T data)
        {
            string filePath = "Assets/[TEST]" + "/" + key + ".msgpack";

            if (File.Exists(filePath))
            {
                byte[] bytes = File.ReadAllBytes(filePath);
                data = MessagePackSerializer.Deserialize<T>(bytes);
            }
            else
            {
                data = default(T);
                Debug.LogWarning($"Файл с ключом '{key}' не найден.");
            }
        }
    }
}