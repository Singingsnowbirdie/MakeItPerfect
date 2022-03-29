using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI looserText;
    [SerializeField] TextMeshProUGUI restartBttnText;
    [SerializeField] TextMeshProUGUI menuBttnText;
    [SerializeField] TextMeshProUGUI hintsCounterText;

    //panels
    GameObject gamePanel;
    GameObject starsPanel;
    GameObject winnerPanel;
    GameObject looserPanel;
    GameObject marketPanel;
    GameObject blackPanel;

    [Header("Effects")]
    [SerializeField] GameObject fireEmbersEffect;
    [SerializeField] GameObject confettiEffect;
    [SerializeField] GameObject starsEffect;

    int correctAns;
    int starsCount;
    int levelCost;
    int levelPenalty;
    int levelNum;
    int hintsCount;
    int coinsCount;

    TextAsset asset;
    XMLSettings UIelement;
    AudioSource clickSound;
    AudioSource confettiSound;
    AudioSource fireSound;
    Canvas canvas;
    GameObject currentLevel;
    GameObject hintsBttn;

    public int LevelNum { get => levelNum; set => levelNum = value; }
    public int HintsCount { get => hintsCount; set => hintsCount = value; }
    public int CoinsCount { get => coinsCount; set => coinsCount = value; }
    public int LevelCost { get => levelCost; set => levelCost = value; }
    public int StarsCount { get => starsCount; set => starsCount = value; }
    public GameObject BlackPanel { get => blackPanel; set => blackPanel = value; }
    public GameObject HintsBttn { get => hintsBttn; set => hintsBttn = value; }
    public Canvas Canvas { get => canvas; set => canvas = value; }

    private void Awake()
    {
        Canvas = FindObjectOfType<Canvas>();

        gamePanel = Canvas.transform.GetChild(0).gameObject;
        winnerPanel = Canvas.transform.GetChild(1).gameObject;
        looserPanel = Canvas.transform.GetChild(2).gameObject;
        HintsBttn = Canvas.transform.GetChild(3).gameObject;
        marketPanel = Canvas.transform.GetChild(4).gameObject;
        blackPanel = Canvas.transform.GetChild(5).gameObject;

        starsPanel = gamePanel.transform.GetChild(1).gameObject;

        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        confettiSound = GameObject.FindGameObjectWithTag("Confetti").GetComponent<AudioSource>();
        fireSound = GameObject.FindGameObjectWithTag("Fire").GetComponent<AudioSource>();

        hintsCount = 3;
    }

    void Start()
    {
        CheckPlayerPrefs();
        LoadNewLevel();
        CheckLanguage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void CheckLanguage()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);

        levelText.text = UIelement.UIelements[0].text + LevelNum;
        looserText.text = UIelement.UIelements[5].text;
        restartBttnText.text = UIelement.UIelements[6].text;
        menuBttnText.text = UIelement.UIelements[7].text;

        CheckHintsCount();
    }

    public void CheckHintsCount()
    {
        HintsCount = PlayerPrefs.GetInt("Hints");

        if (hintsCount > 0)
        {
            hintsCounterText.text = HintsCount.ToString();
        }
        else
        {
            hintsCounterText.text = UIelement.UIelements[11].text;
        }
    }

    public void CorrectAnswer()
    {
        correctAns++;
        CheckLevelProgress();
    }

    public void WrongAnswer()
    {
        StarsCount--;
        LevelCost -= levelPenalty;
        if (StarsCount == 2)
        {
            starsPanel.transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (StarsCount == 1)
        {
            starsPanel.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            WrongBttn bttn = FindObjectOfType<WrongBttn>();
            bttn.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            StartCoroutine(Fail());
        }
    }

    void CheckLevelProgress()
    {
        if (correctAns == currentLevel.GetComponent<LevelController>().Answers)
        {
            StartCoroutine(WinnerEffect());
        }
    }

    public IEnumerator WinnerEffect()
    {
        HintsBttn.GetComponent<CanvasGroup>().blocksRaycasts = false;
        if (PlayerPrefs.HasKey("Extra Level"))
        {
            PlayerPrefs.DeleteKey("Extra Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1f);
        confettiEffect.SetActive(true);
        confettiEffect.GetComponent<ParticleSystem>().Play();
        confettiSound.Play();
        yield return new WaitForSeconds(1f);
        BlackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gamePanel.SetActive(false);
        HintsBttn.SetActive(false);
        winnerPanel.SetActive(true);
        if (StarsCount == 3)
        {
            starsEffect.GetComponent<starFxController>().ea = 3;
            starsEffect.SetActive(true);
        }
        else if (StarsCount == 2)
        {
            starsEffect.GetComponent<starFxController>().ea = 2;
            starsEffect.SetActive(true);
        }
        else if (StarsCount == 1)
        {
            starsEffect.GetComponent<starFxController>().ea = 1;
            starsEffect.SetActive(true);
        }
    }

    public void LoadMenuBttn()
    {
        clickSound.Play();
        fireSound.Stop();
        SceneManager.LoadScene(0);
    }

    public void LoadNewLevel()
    {
        if (looserPanel.activeSelf)
        {
            looserPanel.SetActive(false);
            fireEmbersEffect.SetActive(false);
        }
        if (!gamePanel.activeSelf)
        {
            gamePanel.SetActive(true);
        }
        if (currentLevel)
        {
            Destroy(currentLevel);
        }
        if (PlayerPrefs.GetInt("Hearts") > 0)
        {
            currentLevel = Instantiate(Resources.Load<GameObject>("Prefabs/Levels/Level " + levelNum));
            currentLevel.transform.SetParent(gamePanel.transform, false);
            LevelCost = currentLevel.GetComponent<LevelController>().LevelCost;
            levelPenalty = Convert.ToInt32(LevelCost * 0.3);
            correctAns = 0;
            StarsCount = 3;
            starsPanel.transform.GetChild(0).gameObject.SetActive(true);
            starsPanel.transform.GetChild(1).gameObject.SetActive(true);
            starsPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            marketPanel.SetActive(true);
            marketPanel.GetComponent<MarketPanel>().ItemToBuy = 2;
            marketPanel.GetComponent<MarketPanel>().CheckItem();
        }
    }

    void CheckPlayerPrefs()
    {

        if (PlayerPrefs.HasKey("Extra Level"))
        {
            levelNum = PlayerPrefs.GetInt("Extra Level");
        }
        else if (PlayerPrefs.HasKey("Level"))
        {
            levelNum = PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            levelNum = 1;
        }

        if (!PlayerPrefs.HasKey("Hints"))
        {
            PlayerPrefs.SetInt("Hints", 3);
        }

        if (PlayerPrefs.HasKey("Coins"))
        {
            coinsCount = PlayerPrefs.GetInt("Coins");
        }

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }

        PlayerPrefs.Save();
    }

    public void HintBttn()
    {
        HintsBttn.GetComponent<CanvasGroup>().blocksRaycasts = false;
        StartCoroutine(HintBttnOn());
        clickSound.Play();
        if (HintsCount > 0)
        {
            currentLevel.GetComponent<LevelController>().ShowHint();
        }
        else if (HintsCount == 0)
        {
            StartCoroutine(OpenHintsMarket());
        }
    }

    IEnumerator OpenHintsMarket()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        marketPanel.SetActive(true);
        marketPanel.GetComponent<MarketPanel>().ItemToBuy = 1;
        marketPanel.GetComponent<MarketPanel>().CheckItem();
    }

    IEnumerator HintBttnOn()
    {
        yield return new WaitForSeconds(0.2f);
        HintsBttn.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    IEnumerator Fail()
    {
        if (levelNum > 1)
        {
            PlayerPrefs.SetInt("Hearts", PlayerPrefs.GetInt("Hearts") - 1);
            Debug.Log("Fail. Total Hearts = " + PlayerPrefs.GetInt("Hearts"));
            PlayerPrefs.Save();
        }
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gamePanel.SetActive(false);
        HintsBttn.SetActive(false);
        looserPanel.SetActive(true);
        fireEmbersEffect.SetActive(true);
        fireEmbersEffect.GetComponent<ParticleSystem>().Play();
        fireSound.Play();
    }

    public IEnumerator CloseMarketAndCheckHearts()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        marketPanel.SetActive(false);
        LoadNewLevel();
    }
}
