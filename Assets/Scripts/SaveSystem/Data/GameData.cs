using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<string> unlockedCards;

    public GameData()
    {
        unlockedCards = new List<string>();
    }
}
