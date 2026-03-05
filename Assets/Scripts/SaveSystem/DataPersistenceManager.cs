using System.Security.Cryptography;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName;

    public static DataPersistenceManager Instance { get; private set; }

    private GameData _gameData;
    private List<IDataPersistence> _dataScripts;
    private FileDataHandler _dataHandler;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There are multiple Data Persistence Managers!");
            return;
        }
        Instance = this; 
        _dataScripts = new List<IDataPersistence>();
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        LoadGameData();
    }

    public void StartNewGame()
    {
        _gameData = new GameData();
    }

    public void StartNewRun()
    {
        
    }

    public void LoadGameData()
    {
        Debug.Log("Load!");
        this._gameData = _dataHandler.Load();

        if (this._gameData == null)
        {
            StartNewGame();
        }
    }

    public void GiveObjectData(IDataPersistence obj)
    {
        Debug.Log("Object Data Loaded!");
        obj.LoadData(_gameData);
    }

    public void DeleteSave()
    {
        
    }

    public void SaveNewCardUnlock()
    {
        foreach (IDataPersistence data in _dataScripts)
        {
            data.SaveData(ref _gameData);
        }

        foreach (string card in _gameData.unlockedCards)
        {
            Debug.Log("Cards Unlocked: " + card);
        }

        _dataHandler.Save(_gameData);
    }

    public void Register(IDataPersistence data)
    {
        _dataScripts.Add(data);
        GiveObjectData(data);
    }

    public void Unregister(IDataPersistence data)
    {
        _dataScripts.Remove(data);
    }
}
