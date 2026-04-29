using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;

    
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioSource bgmSource;
    [SerializeField] List<AudioClip> sfxClips = new List<AudioClip>();
    [SerializeField] List<AudioClip> bgmClips = new List<AudioClip>();

    public const string Music_Key = "musicVol";
    public const string SFX_Key = "sfxVol";  //_
    
    private int currentClip;

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

}
