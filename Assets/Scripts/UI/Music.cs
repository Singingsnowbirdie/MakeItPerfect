using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public static Music instance = null;
    AudioSource music;
    [SerializeField] AudioMixerGroup mixer;

    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        if (instance != null)
        {
            Destroy(gameObject);
            Debug.Log("One Music Destroyed");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        CheckMusicOptions();
    }

    public void CheckMusicOptions()
    {
        if (!PlayerPrefs.HasKey("MusicEnabled"))
        {
            PlayerPrefs.SetInt("MusicEnabled", 1);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetInt("MusicEnabled") == 1)
            {
                mixer.audioMixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);

        }
        else if (PlayerPrefs.GetInt("MusicEnabled") == 0)
            {
                mixer.audioMixer.SetFloat("MusicVol", -80);
            }
    }

    public void Pause()
    {
        music.Pause();
    }

    public void Play()
    {
        music.Play();
    }
}