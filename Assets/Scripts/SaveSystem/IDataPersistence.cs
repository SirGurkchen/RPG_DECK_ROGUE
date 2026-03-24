/// <summary>
/// Interface for Objects which have data to be saved.
/// </summary>
public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
