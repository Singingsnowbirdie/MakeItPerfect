using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Sounds : MonoBehaviour
{
    public static Sounds instance = null;
    [SerializeField] AudioMixerGroup mixer;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.Log("One Sounds Destroyed");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        CheckSoundsOptions();
    }

    public void CheckSoundsOptions()
    {
        if (!PlayerPrefs.HasKey("SoundsEnabled"))
        {
            PlayerPrefs.SetInt("SoundsEnabled", 1);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("SoundsVolume"))
        {
            PlayerPrefs.SetFloat("SoundsVolume", 1);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("SoundsEnabled"))
        {
            if (PlayerPrefs.GetInt("SoundsEnabled") == 1)
            {
                mixer.audioMixer.SetFloat("SoundsVol", Mathf.Log10(PlayerPrefs.GetFloat("SoundsVolume")) * 20);
            }
            else if (PlayerPrefs.GetInt("SoundsEnabled") == 0)
            {
                mixer.audioMixer.SetFloat("SoundsVol", -80);
            }
        }
    }
}