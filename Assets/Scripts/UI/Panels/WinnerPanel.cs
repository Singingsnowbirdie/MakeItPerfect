using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class WinnerPanel : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TextMeshProUGUI doubleCountText;
    [SerializeField] TextMeshProUGUI countBttnText;
    [SerializeField] TextMeshProUGUI doubleCountBttnText;

    GameManager game;

    int coins;

    TextAsset asset;
    XMLSettings UIelement;

    AudioSource clickSound;

    public int Coins { get => coins; set => coins = value; }

    private void Awake()
    {
        game = FindObjectOfType<GameManager>();
        Coins = game.LevelCost;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    void Start()
    {
        CheckLanguage();
    }

    public void CheckLanguage()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);

        countBttnText.text = UIelement.UIelements[8].text;
        doubleCountBttnText.text = UIelement.UIelements[9].text;

        countText.text = Coins.ToString();
        doubleCountText.text = (Coins * 2).ToString();
    }

    public void RewardBttn()
    {
        clickSound.Play();
        int currentCoins = PlayerPrefs.GetInt("Coins") + Coins;
        PlayerPrefs.SetInt("Coins", currentCoins);
        PlayerPrefs.Save();
SceneManager.LoadScene(0);
    }

}
