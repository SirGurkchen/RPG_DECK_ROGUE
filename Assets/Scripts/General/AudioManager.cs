using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _soundEffectSource;
    [SerializeField] private AudioSource _musicSource;

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

    public void PlayAudioClip(AudioClip sound)
    {
        if (sound == null) return;
        _soundEffectSource.PlayOneShot(sound);
    }

    public void StartMusic(AudioClip music)
    {
        if (music == null) return;
        _musicSource.loop = true;
        _musicSource.PlayOneShot(music);
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
