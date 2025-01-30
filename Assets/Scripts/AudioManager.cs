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
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void playMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    // Update is called once per frame
    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void stopPlaySFX()
    {
        SFXSource.Stop();
    }
    public bool isSFXPlaying()
    {
        sfxIsPlaying = SFXSource.isPlaying;
        return sfxIsPlaying;
    }
}
