using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;

    
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioSource bgmSource;
    [SerializeField] public AudioSource ambienceSource;
    [SerializeField] List<AudioClip> sfxClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> bgmClips = new List<AudioClip>();

    public const string Music_Key = "musicVol";
    public const string SFX_Key = "sfxVol";  //_
    
    private int currentClip;

    private bool stopped;

    void Awake()
    {
        if (instance == null)
        {
            instance = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //LoadVolume();
    }

    void LoadVolume()
    {
        float musicVol = PlayerPrefs.GetFloat(Music_Key, 1f);
        float sfxVol = PlayerPrefs.GetFloat(SFX_Key, 1f);

        mixer.SetFloat(VolumeSettings.Mixer_Music, Mathf.Log10(musicVol) * 20);
        mixer.SetFloat(VolumeSettings.Mixer_SFX, Mathf.Log10(sfxVol) * 20);
    }

    void Start()
    {
        bgmSource.Play();
    }

    public void PlaySFX(int num)
    {
        AudioClip clip = sfxClips[num];
        sfxSource.PlayOneShot(clip);
    }

    public void StopSFX(int num)
    {
        AudioClip clip = sfxClips[num];
        sfxSource.Stop();
    }

    public void PlayBGM(int num)
    {
        AudioClip clip = bgmClips[num];
        bgmSource.clip = clip;
        bgmSource.Play();

        currentClip = num;
    }

    public int CurrentBGM()
    {
        return currentClip;
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void StopAll()
    {
        bgmSource.Stop();
        sfxSource.Stop();
        ambienceSource.Stop();

        stopped = true;
    }

    public void PlayAll()
    {
        bgmSource.Play();
        sfxSource.Play();
        ambienceSource.Play();

        stopped = false;
    }


    public void MuteButton()
    {
        if (stopped == true)
        {
            PlayAll();
        }
        if(stopped == false)
        {
            StopAll();
        }
    }

}
