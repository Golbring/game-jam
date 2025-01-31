using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource SFXSource;

    [SerializeField]
    Slider musicVolumeSlider;
    [SerializeField]
    Slider sfxVolumeSlider;

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
       
    }
    private void Start()
    {

            PlayerPrefs.SetFloat("musicVolume", 0.5f);

            PlayerPrefs.SetFloat("sfxVolume", 0.5f);

    }
    public void ChangeMusicVolume()
    {
        musicSource.volume = musicVolumeSlider.value;
        //Save();
    }
    public void ChangeSfxVolume()
    {
        SFXSource.volume = sfxVolumeSlider.value;
        //Save();
    }

    public void playMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    private void Load()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
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
