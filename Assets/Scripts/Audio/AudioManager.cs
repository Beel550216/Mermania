using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;

    
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] List<AudioClip> sfxClips = new List<AudioClip>();
    

    public const string Music_Key = "musicVol";
    public const string SFX_Key = "sfxVol";  //_
    

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
        //LoadVloume();
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
        backgroundSource.Play();
    }

    public void playSFX(int num)
    {
        AudioClip clip = sfxClips[num];
        sfxSource.PlayOneShot(clip);
    }

}
