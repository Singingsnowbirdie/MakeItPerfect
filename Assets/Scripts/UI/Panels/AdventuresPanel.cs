using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class AdventuresPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI part1Text;
    [SerializeField] TextMeshProUGUI part2Text;

    [SerializeField] TextMeshProUGUI yesBttnText;
    [SerializeField] TextMeshProUGUI noBttnText;

    [SerializeField] TextMeshProUGUI adventuresCountText;


    TextAsset asset;
    XMLSettings UIelement;

    MenuController menu;

    int level;

    GameObject startBttnPanel;
    GameObject blackPanel;

    AudioSource clickSound;

    bool adventuresTimer;

    private void Awake()
    {
        menu = FindObjectOfType<MenuController>();
        level = menu.Level;
        startBttnPanel = transform.GetChild(4).gameObject;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    void Start()
    {
        blackPanel = menu.BlackPanel;
        CheckLanguage();
        CheckTimer();
    }

    private void Update()
    {
        if (adventuresTimer)
        {
            DateTime currentDate = DateTime.UtcNow.ToLocalTime();
            DateTime newAdventureDate = DateTime.Parse(PlayerPrefs.GetString("Adventure Date"));
            TimeSpan turnTimeRemaining = newAdventureDate - currentDate;
            adventuresCountText.text = turnTimeRemaining.Hours + " : " + turnTimeRemaining.Minutes + " : " + turnTimeRemaining.Seconds;
            CheckTimer();
        }
    }


    public void CheckLanguage()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);

        yesBttnText.text = UIelement.UIelements[19].text;
        noBttnText.text = UIelement.UIelements[21].text;
    }

    public void CheckTimer()
    {
        if (!PlayerPrefs.HasKey("Adventure Date"))
        {
            PlayerPrefs.SetInt("Adventures", 3);
            PlayerPrefs.SetString("Adventure Date", DateTime.UtcNow.ToLocalTime().AddHours(24).ToString());
        }
        else if (PlayerPrefs.HasKey("Adventure Date"))
        {
            DateTime currentDate = DateTime.UtcNow.ToLocalTime();
            DateTime dateForComparison = DateTime.Parse(PlayerPrefs.GetString("Adventure Date"));
            if (currentDate > dateForComparison)
            {
                PlayerPrefs.SetInt("Adventures", 3);
                PlayerPrefs.SetString("Adventure Date", DateTime.UtcNow.ToLocalTime().AddHours(24).ToString());
            }
        }
        if (PlayerPrefs.GetInt("Adventures") < 1)
        {
            part1Text.text = UIelement.UIelements[27].text;
            part2Text.transform.parent.gameObject.SetActive(false);
            adventuresTimer = true;
            startBttnPanel.SetActive(false);
        }
        else
        {
            adventuresTimer = false;
            part1Text.text = UIelement.UIelements[17].text;
            part2Text.transform.parent.gameObject.SetActive(true);
            part2Text.text = UIelement.UIelements[18].text;
            int attemptsCount = PlayerPrefs.GetInt("Adventures");
            adventuresCountText.text = attemptsCount.ToString() + "/3";
            startBttnPanel.SetActive(true);
        }
    }

    public void StartAdventureBttn()
    {
        clickSound.Play();
        int extraLevel = Random.Range(1, level);
        PlayerPrefs.SetInt("Extra Level", extraLevel);
        PlayerPrefs.SetInt("Adventures", PlayerPrefs.GetInt("Adventures") - 1);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void ClosePanelBttn()
    {
        clickSound.Play();
        StartCoroutine(ExitPanel());
    }

    IEnumerator ExitPanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
