using System;
using UnityEngine;

/// <summary>
/// Stores the Round Buffers the game has.
/// Chooses which next round buffer is being played.
/// No Round buffer can be chosen more than twice in a row.
/// </summary>
public class RoundBufferPool : MonoBehaviour
{
    public static RoundBufferPool Instance { get; private set; }

    private int _chosenTimes = 0;
    private BufferType _previousChosen;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There are multiple Round Buffer Pools!");
            return;
        }
        Instance = this;
    }

    public enum BufferType
    {
        Shop,
        Reward
    }

    /// <summary>
    /// Determines a random round buffer.
    /// Same round buffer cannot be returned twice in a row.
    /// </summary>
    /// <returns>Selected round buffer.</returns>
    public BufferType GetRandomRoundBuffer()
    {
        if (CheckRepeated())
        {
            _chosenTimes = 0;
            return ChooseOther(_previousChosen);
        }

        BufferType[] types = (BufferType[])Enum.GetValues(typeof(BufferType));
        BufferType chosen = types[UnityEngine.Random.Range(0, types.Length)];

        if (_previousChosen == chosen)
        {
            _chosenTimes++;
        }
        else
        {
            _chosenTimes = 0;
        }

        _previousChosen = chosen;
        return chosen;
    }

    private bool CheckRepeated()
    {
        return _chosenTimes >= 1;
    }

    private BufferType ChooseOther(BufferType chosen)
    {
        switch (chosen)
        {
            case BufferType.Shop:
                return BufferType.Reward;
            case BufferType.Reward:
                return BufferType.Shop;
            default:
                return BufferType.Reward;
        }
    }
}
