using System.Collections.Generic;

/// <summary>
/// Handles the definiton of Data that can be saved.
/// </summary>
[System.Serializable]
public class GameData
{
    public List<string> unlockedCards;

    /// <summary>
    /// Creates new game data.
    /// </summary>
    public GameData()
    {
        unlockedCards = new List<string>();
    }
}
