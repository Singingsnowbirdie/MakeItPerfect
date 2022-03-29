using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MarketPanel : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI tooltipText;
    [SerializeField] TextMeshProUGUI coinsAmountText;
    [SerializeField] TextMeshProUGUI hintsAmountText;
    [SerializeField] TextMeshProUGUI heartsAmountText;
    [SerializeField] TextMeshProUGUI showAdsBttnText;
    [SerializeField] TextMeshProUGUI showAdsBttnText2;
    [SerializeField] TextMeshProUGUI buyText;
    [SerializeField] TextMeshProUGUI forText;
    [SerializeField] TextMeshProUGUI unitsToBuyText;
    [SerializeField] TextMeshProUGUI coinsToPayText;
    [SerializeField] TextMeshProUGUI exitBttnText;

    [Header("Elements")]
    [SerializeField] GameObject playerHintsPanel;
    [SerializeField] GameObject playerHeartsPanel;
    [SerializeField] GameObject hintImg;
    [SerializeField] GameObject heartImg;
    [SerializeField] GameObject hint4AdsBttn;
    [SerializeField] GameObject heart4AdsBttn;

    TextAsset asset;
    XMLSettings UIelement;

    int itemToBuy;
    int hints;
    int coins;
    int unitsToBuy = 1;
    int coinsToPay;

    GameManager game;
    GameObject blackPanel;

    AudioSource clickSound;

    public int ItemToBuy { get => itemToBuy; set => itemToBuy = value; }
    public int UnitsToBuy { get => unitsToBuy; set => unitsToBuy = value; }
    public int CoinsToPay { get => coinsToPay; set => coinsToPay = value; }
    public TextMeshProUGUI TooltipText { get => tooltipText; set => tooltipText = value; }

    private void Awake()
    {
        game = FindObjectOfType<GameManager>();
        blackPanel = game.BlackPanel;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    void Start()
    {
        CheckItem();
    }

    public void CheckItem()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);

        if (itemToBuy == 1)//подсказки
        {
            TooltipText.text = "";
            playerHeartsPanel.SetActive(false);
            heart4AdsBttn.SetActive(false);
            heartImg.SetActive(false);
            playerHintsPanel.SetActive(true);
            hint4AdsBttn.SetActive(true);
            hintImg.SetActive(true);
            CoinsToPay = 7;
            ShowHintsAmount();
        }

        else if (itemToBuy == 2)//жизни
        {
            playerHintsPanel.SetActive(false);
            hint4AdsBttn.SetActive(false);
            hintImg.SetActive(false);
            playerHeartsPanel.SetActive(true);
            heart4AdsBttn.SetActive(true);
            heartImg.SetActive(true);
            CoinsToPay = 21;
            ShowHeartsAmount();
        }

        showAdsBttnText.text = UIelement.UIelements[10].text;
        showAdsBttnText2.text = UIelement.UIelements[10].text;
        buyText.text = UIelement.UIelements[11].text;
        forText.text = UIelement.UIelements[13].text;
        exitBttnText.text = UIelement.UIelements[21].text;
        unitsToBuyText.text = unitsToBuy.ToString();
        coinsToPayText.text = CoinsToPay.ToString();

        ShowCoinsAmount();
        ShowCoinsToPay();
    }
    public void ShowCoinsAmount()
    {
        coinsAmountText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void ShowHintsAmount()
    {
        hint4AdsBttn.SetActive(false);
        hint4AdsBttn.SetActive(true);
        hintsAmountText.text = PlayerPrefs.GetInt("Hints").ToString();
    }
    public void ShowHeartsAmount()
    {
        if (PlayerPrefs.GetInt("Hearts") == 0)
        {
            TooltipText.text = UIelement.UIelements[14].text;
        }
        else
        {
            TooltipText.text = UIelement.UIelements[29].text;
        }
        heart4AdsBttn.SetActive(false);
        heart4AdsBttn.SetActive(true);
        heartsAmountText.text = PlayerPrefs.GetInt("Hearts").ToString();
    }

    public void ShowCoinsToPay()
    {
        coinsToPayText.text = CoinsToPay.ToString();
    }

    public void ShowUnitsToBuy()
    {
        unitsToBuyText.text = unitsToBuy.ToString();
        if (itemToBuy == 1)
        {
            CoinsToPay = unitsToBuy * 7;
        }
        else if (itemToBuy == 2)
        {
            CoinsToPay = unitsToBuy * 21;
        }
        ShowCoinsToPay();
    }
    public void BackToGameBttn()
    {
        clickSound.Play();
        if (itemToBuy == 1)
        {
            StartCoroutine(ClosePanel());
        }
        else if (itemToBuy == 2)
        {
            StartCoroutine(game.CloseMarketAndCheckHearts());
        }
    }

    IEnumerator ClosePanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
