using System.Security.Cryptography;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance { get; private set; }

    private GameData _gameData;
    private List<IDataPersistence> _dataScripts;

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
        if (this._gameData == null)
        {
            Debug.Log("Starting New Game");
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

    public void SaveNewCardUnlock(ItemBase itemCard)
    {
        foreach (IDataPersistence data in _dataScripts)
        {
            data.SaveData(ref _gameData);
        }
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataObjects = FindObjectsByType<MonoBehaviour>(0).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataObjects);
    }
}
