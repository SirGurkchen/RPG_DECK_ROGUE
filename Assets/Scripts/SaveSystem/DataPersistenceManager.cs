using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Handles the saving of Data of the game.
/// Elements that save something register to this class.
/// </summary>
public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance { get; private set; }

    [Header("File Storage Config")]
    [SerializeField] private string _fileName;
    [SerializeField] private bool _useEncryption;

    private GameData _gameData;
    private List<IDataPersistence> _dataScripts;
    private FileDataHandler _dataHandler;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _dataScripts = new List<IDataPersistence>();
        _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
        LoadGameData();
    }

    /// <summary>
    /// Starts a new game save file.
    /// </summary>
    public void StartNewGame()
    {
        _gameData = new GameData();
    }

    /// <summary>
    /// Loads an existing save file.
    /// </summary>
    public void LoadGameData()
    {
        this._gameData = _dataHandler.Load();

        if (this._gameData == null)
        {
            StartNewGame();
        }
    }

    /// <summary>
    /// Loads data to registered objects.
    /// </summary>
    /// <param name="obj"></param>
    public void GiveObjectData(IDataPersistence obj)
    {
        obj.LoadData(_gameData);
    }

    /// <summary>
    /// Saves a new card unlock.
    /// </summary>
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

    /// <summary>
    /// Registeres a new object that saves.
    /// </summary>
    /// <param name="data">Object that saves.</param>
    public void Register(IDataPersistence data)
    {
        _dataScripts.Add(data);
        GiveObjectData(data);
    }

    /// <summary>
    /// Unregisteres an object that saves.
    /// </summary>
    /// <param name="data">Object that saves.</param>
    public void Unregister(IDataPersistence data)
    {
        _dataScripts.Remove(data);
    }
}
