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
        }
        Instance = this; 
    }

    private void Start()
    {
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        this._dataScripts = FindAllDataPersistenceObjects();
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
        this._gameData = _dataHandler.Load();

        if (this._gameData == null)
        {
            StartNewGame();
        }

        foreach (IDataPersistence data in _dataScripts)
        {
            data.LoadData(_gameData);
        }
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

        foreach (ItemBase card in _gameData.unlockedCards)
        {
            Debug.Log("Cards Unlocked: " + card.UnlockCard.GetCard().Name);
        }

        _dataHandler.Save(_gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataObjects = FindObjectsByType<MonoBehaviour>(0).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataObjects);
    }
}
