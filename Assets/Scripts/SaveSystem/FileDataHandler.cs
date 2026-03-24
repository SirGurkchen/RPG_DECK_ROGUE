using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Handles encrypting save data and writing it into a save file.
/// Decrypt the data on loading.
/// </summary>
public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";
    private bool _useEncryption = false;
    private readonly string _encryptionCodeWord = "❄︎♒︎♓︎⬧︎🏱♋︎⬧︎⬧︎⬥︎□︎❒︎♎︎🕈︎□︎❒︎♎︎✋︎⬧︎❖︎♏︎❒︎⍓︎☹︎□︎■︎♑︎⚐︎■︎🏱︎◆︎❒︎◻︎□︎⬧︎♏︎❄︎□︎💣︎♋︎🙵♏︎💧︎♋︎❖︎♓︎■︎♑︎☜︎■︎♍︎❒︎⍓︎◻︎⧫︎♓︎□︎■︎💣︎□︎❒︎💧︎♋︎❖︎♏︎";

    /// <summary>
    /// Creates a new FileDataHandler.
    /// </summary>
    /// <param name="dataDirPath">Save Path</param>
    /// <param name="dataFileName">Save file name./</param>
    /// <param name="useEncryption">Determines if file should be encrypted.</param>
    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this._dataDirPath = dataDirPath;
        this._dataFileName = dataFileName;
        _useEncryption = useEncryption;
    }

    /// <summary>
    /// Reads the save file and return game data.
    /// </summary>
    /// <returns>Loaded game data.</returns>
    public GameData Load()
    {
        // Path.Combine to assure saving works in different OS's
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error while Loading Game Data!" + "\n" + e);
            }
        }
        return loadedData;
    }

    /// <summary>
    /// Writes to the save file.
    /// </summary>
    /// <param name="data">Data to save.</param>
    public void Save(GameData data)
    {
        // Path.Combine to assure saving works in different OS's
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while trying to save the game to the file: " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ _encryptionCodeWord[i % _encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
