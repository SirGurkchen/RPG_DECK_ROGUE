using UnityEngine;

/// <summary>
/// Handles the settings of the game.
/// Centralizes the adjustment of settings.
/// </summary>
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

    /// <summary>
    /// Increases effect volume.
    /// </summary>
    public void ChangeEffectVolumePlus()
    {
        AudioManager.Instance.TryChangeEffectVolume(0.1f);
    }

    /// <summary>
    /// Decreases effect volume.
    /// </summary>
    public void ChangeEffectVolumeMinus()
    {
        AudioManager.Instance.TryChangeEffectVolume(-0.1f);
    }

    /// <summary>
    /// Increases music volume.
    /// </summary>
    public void ChangeMusicVolumePlus()
    {
        AudioManager.Instance.TryChangeMusicVolume(0.1f);
    }
    
    /// <summary>
    /// Decreases music volume.
    /// </summary>
    public void ChangeMusicVolumeMinus()
    {
        AudioManager.Instance.TryChangeMusicVolume(-0.1f);
    }
}
