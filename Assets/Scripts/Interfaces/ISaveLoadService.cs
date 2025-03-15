namespace Interfaces
{
    public interface ISaveLoadService
    {
        void Save(string key, object data);
        void Load<T>(string key, out T data);
    }
}