using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public static SaveManager Instance;

    private SaveData _gameData;
    private FileDataHandler _dataHandler;

    private List<IDataPersistance> _dataPersistanceObjects;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        _dataPersistanceObjects = FindAllDataPersistanceObjects();
        NewGame();

    }

    void Update()
    {
        
    }

    public void NewGame()
    {
        _gameData = new SaveData();
    }

    public void Save()
    {
        foreach (IDataPersistance persistanceObject in _dataPersistanceObjects)
        {
            persistanceObject.SaveData(ref _gameData);
        }

        _dataHandler.Save(_gameData);
    }

    public void Load()
    {
        _gameData = _dataHandler.Load();

        if( _gameData == null)
        {
            NewGame();
            Debug.Log("No Data to load, initializing to defaults.");
        }

        foreach(IDataPersistance persistanceObject in _dataPersistanceObjects)
        {
            persistanceObject.LoadData(_gameData);
        }
        
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }


    public SaveData GameData { get { return _gameData; } }
}
