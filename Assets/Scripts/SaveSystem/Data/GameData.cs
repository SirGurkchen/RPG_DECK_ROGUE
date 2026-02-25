using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<ItemBase> unlockedCards;

    public GameData()
    {
        unlockedCards = new List<ItemBase>();
    }
}
