using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeEffectVolumePlus()
    {
        AudioManager.Instance.TryChangeEffectVolume(0.1f);
    }

    public void ChangeEffectVolumeMinus()
    {
        AudioManager.Instance.TryChangeEffectVolume(-0.1f);
    }
}
