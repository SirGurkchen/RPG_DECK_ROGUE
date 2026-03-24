using UnityEngine;

/// <summary>
/// Handles playing and stopping music and sound effects in game.
/// Volume for music and sound effects are adjusted here.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _soundEffectSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _errorSound;
    [SerializeField] private AudioClip _chainSound;
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip _sceneChangeSound;
    [SerializeField] private AudioClip _defaultMusic;

    public float EffectVolume => _soundEffectSource.volume;
    public float MusicVolume => _musicSource.volume;

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
    /// Checks if a sound is finished playing.
    /// </summary>
    /// <returns>True if sound is finished.</returns>
    public bool IsSoundFinished()
    {
        return !_soundEffectSource.isPlaying;
    }

    /// <summary>
    /// Plays the scene change effect sound.
    /// </summary>
    public void PlayChangeSound()
    {
        _soundEffectSource.PlayOneShot(_sceneChangeSound);
    }

    /// <summary>
    /// Plays the chain sound effect.
    /// </summary>
    public void PlayChainSound()
    {
        _soundEffectSource.PlayOneShot(_chainSound);
    }

    /// <summary>
    /// Plays the button press sound effect.
    /// </summary>
    public void PlayButtonPress()
    {
        _soundEffectSource.PlayOneShot(_buttonSound);
    }

    /// <summary>
    /// Plays a given sound effect once.
    /// </summary>
    /// <param name="sound">Given sound effect.</param>
    public void PlayAudioClip(AudioClip sound)
    {
        if (sound == null) return;
        _soundEffectSource.PlayOneShot(sound);
    }

    /// <summary>
    /// Plays the error sound effect.
    /// </summary>
    public void PlayErrorSound()
    {
        _soundEffectSource.PlayOneShot(_errorSound);
    }

    /// <summary>
    /// Starts playing the default music and loops it.
    /// </summary>
    public void PlayDefaultMusic()
    {
        _musicSource.loop = true;
        _musicSource.clip = _defaultMusic;
        _musicSource.Play();
    }

    /// <summary>
    /// Stops the music.
    /// </summary>
    public void StopMusic()
    {
        _musicSource.Stop();
    }

    /// <summary>
    /// Tries changing the volume of sound effects.
    /// If min or max is reached does not change.
    /// </summary>
    /// <param name="change">Volume change.</param>
    public void TryChangeEffectVolume(float change)
    {
        if (change >= 0.0f && _soundEffectSource.volume == 1.0f) return;
        if (change <= 0.0f && _soundEffectSource.volume == 0.0f) return;
        _soundEffectSource.volume += change;
    }

    /// <summary>
    /// Tries changing the volume of music.
    /// If min or max is reached does not change.
    /// </summary>
    /// <param name="change">Volume change.</param>
    public void TryChangeMusicVolume(float change)
    {
        if (change >= 0.0f && _musicSource.volume == 1.0f) return;
        if (change <= 0.0f && _musicSource.volume == 0.0f) return;
        _musicSource.volume += change;
    }
}
