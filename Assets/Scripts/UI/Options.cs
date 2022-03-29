using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI selectLanguageText;
    [SerializeField] TextMeshProUGUI musicVolumeText;
    [SerializeField] TextMeshProUGUI soundsVolumeText;
    [SerializeField] TextMeshProUGUI musicToggleText;
    [SerializeField] TextMeshProUGUI soundsToggleText;
    [SerializeField] TextMeshProUGUI exitBttnText;
    [SerializeField] TextMeshProUGUI creditsBttnText;

    [Header("Toggles")]
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle soundsToggle;

    [Header("Sliders")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundsSlider;

    AudioSource clickSound;
    Music music;
    Sounds sounds;
    TextAsset asset;
    XMLSettings UIelement;
    MenuController menu;
    GameObject blackPanel;
    GameObject creditsPanel;
    ObjectivesPanel objectives;

    private void Awake()
    {
        menu = FindObjectOfType<MenuController>();
        blackPanel = menu.BlackPanel;
        creditsPanel = menu.CreditsPanel;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        music = FindObjectOfType<Music>();
        sounds = FindObjectOfType<Sounds>();
        objectives = FindObjectOfType<ObjectivesPanel>();
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("MusicEnabled") == 1)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            musicToggle.isOn = true;
        }
        else if (PlayerPrefs.GetInt("MusicEnabled") == 0)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            musicToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("SoundsEnabled") == 1)
        {
            soundsSlider.value = PlayerPrefs.GetFloat("SoundsVolume");
            soundsToggle.isOn = true;
        }
        else if (PlayerPrefs.GetInt("SoundsEnabled") == 0)
        {
            soundsSlider.value = PlayerPrefs.GetFloat("SoundsVolume");
            soundsToggle.isOn = false;
        }
        CheckLanguage();
    }
    public void SetRusBttn()
    {
        clickSound.Play();
        LocalizationManager.currentLanguage = "ru_RU";
        PlayerPrefs.SetString("Language", "ru_RU");
        PlayerPrefs.Save();
        ChangeLang();
    }
    public void SetEngBttn()
    {
        clickSound.Play();
        LocalizationManager.currentLanguage = "en_US";
        PlayerPrefs.SetString("Language", "en_US");
        PlayerPrefs.Save();
        ChangeLang();
    }
    void CheckLanguage()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/Options");
        UIelement = XMLSettings.Load(asset);

        selectLanguageText.text = UIelement.UIelements[0].text;
        musicVolumeText.text = UIelement.UIelements[1].text;
        soundsVolumeText.text = UIelement.UIelements[2].text;
        musicToggleText.text = UIelement.UIelements[3].text;
        soundsToggleText.text = UIelement.UIelements[4].text;
        exitBttnText.text = UIelement.UIelements[5].text;
        creditsBttnText.text = UIelement.UIelements[6].text;
    }
    public void MusicToggle(bool enabled)
    {
        clickSound.Play();
        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
        music.CheckMusicOptions();
    }
    public void SoundsToggle(bool enabled)
    {
        clickSound.Play();
        PlayerPrefs.SetInt("SoundsEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
        sounds.CheckSoundsOptions();
    }

    public void ChangeMusicVolume(float volume)
    {
        if (PlayerPrefs.GetInt("MusicEnabled") == 0)
        {
            PlayerPrefs.SetInt("MusicEnabled", 1);
            musicToggle.isOn = true;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        music.CheckMusicOptions();
    }

    public void ChangeSoundsVolume(float volume)
    {
        if (PlayerPrefs.GetInt("SoundsEnabled") == 0)
        {
            PlayerPrefs.SetInt("SoundsEnabled", 1);
            soundsToggle.isOn = true;
        }
        PlayerPrefs.SetFloat("SoundsVolume", volume);
        PlayerPrefs.Save();
        sounds.CheckSoundsOptions();
    }

    public void ExitBttn()
    {
        clickSound.Play();
        StartCoroutine(ClosePanel());
    }

    IEnumerator ClosePanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    public void creditsBttn()
    {
        clickSound.Play();
        StartCoroutine(OpenCreditsPanel());
    }

    IEnumerator OpenCreditsPanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        creditsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ChangeLang()
    {
        CheckLanguage();
        menu.CheckLanguage();
        objectives.CheckLanguage();
    }

}
