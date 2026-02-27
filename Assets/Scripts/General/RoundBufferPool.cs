using System;
using UnityEngine;

public class RoundBufferPool : MonoBehaviour
{
    public static RoundBufferPool Instance { get; private set; }

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
        BufferType[] types = (BufferType[])Enum.GetValues(typeof(BufferType));
        return types[UnityEngine.Random.Range(0, types.Length)];
    }
}
