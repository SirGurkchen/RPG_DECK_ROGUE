using System;
using UnityEngine;

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
