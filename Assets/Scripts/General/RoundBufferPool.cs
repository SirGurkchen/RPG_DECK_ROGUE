using System;
using UnityEngine;

public class RoundBufferPool : MonoBehaviour
{
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
