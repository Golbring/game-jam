using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource SFXSource;

    [Header("--- Audio Clip ---")]
    public AudioClip backgroundMusic;
    public AudioClip menuMusic;
    public AudioClip slingDraw;
    public AudioClip slingRelease;
    public AudioClip flying;
    public AudioClip landing;
    public AudioClip hitEnemy;
    public AudioClip clickButton;

    private bool sfxIsPlaying;
    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void stopPlaySFX(AudioClip clip)
    {
        SFXSource.Stop();
    }
    public bool isSFXPlaying()
    {
        sfxIsPlaying = SFXSource.isPlaying;
        return sfxIsPlaying;
    }
}
