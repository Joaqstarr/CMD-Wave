public interface IDataPersistance 
{
    public void SaveData(ref SaveData data);

    public void LoadData(SaveData data);
}
