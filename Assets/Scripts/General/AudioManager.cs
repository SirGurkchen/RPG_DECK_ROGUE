using UnityEngine;

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

    public bool IsSoundFinished()
    {
        return !_soundEffectSource.isPlaying;
    }

    public void PlayChangeSound()
    {
        _soundEffectSource.PlayOneShot(_sceneChangeSound);
    }

    public void PlayChainSound()
    {
        _soundEffectSource.PlayOneShot(_chainSound);
    }

    public void PlayButtonPress()
    {
        _soundEffectSource.PlayOneShot(_buttonSound);
    }

    public void PlayAudioClip(AudioClip sound)
    {
        if (sound == null) return;
        _soundEffectSource.PlayOneShot(sound);
    }

    public void PlayErrorSound()
    {
        _soundEffectSource.PlayOneShot(_errorSound);
    }

    public void PlayDefaultMusic()
    {
        _musicSource.loop = true;
        _musicSource.clip = _defaultMusic;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void TryChangeEffectVolume(float change)
    {
        if (change >= 0.0f && _soundEffectSource.volume == 1.0f) return;
        if (change <= 0.0f && _soundEffectSource.volume == 0.0f) return;
        _soundEffectSource.volume += change;
    }

    public void TryChangeMusicVolume(float change)
    {
        if (change >= 0.0f && _musicSource.volume == 1.0f) return;
        if (change <= 0.0f && _musicSource.volume == 0.0f) return;
        _musicSource.volume += change;
    }
}
