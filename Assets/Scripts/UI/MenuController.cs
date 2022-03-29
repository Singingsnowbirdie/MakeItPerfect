using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI currentHeartsText;
    [SerializeField] TextMeshProUGUI currentHintsText;
    [SerializeField] TextMeshProUGUI currentCoinsText;
    [SerializeField] TextMeshProUGUI StartBttnText;

    //panels
    GameObject heartsPanel;
    GameObject navigationPanel;
    GameObject objectivesPanel;
    GameObject startGameBttnPanel;
    GameObject adventuresPanel;
    GameObject newMessagePanel;
    GameObject messagePanel;
    GameObject improvementsPanel;
    GameObject raidsPanel;
    GameObject optionsPanel;
    GameObject creditsPanel;
    GameObject blackPanel;

    GameObject messageBttnPrefab;

    TextAsset asset;
    XMLSettings UIelement;

    Canvas canvas;

    int level;

    public GameObject RewardsPanel { get => newMessagePanel; set => newMessagePanel = value; }
    public int Level { get => level; set => level = value; }
    public GameObject NavigationPanel { get => navigationPanel; set => navigationPanel = value; }
    public GameObject ImprovementsPanel { get => improvementsPanel; set => improvementsPanel = value; }
    public GameObject MessagePanel { get => messagePanel; set => messagePanel = value; }
    public GameObject AdventuresPanel { get => adventuresPanel; set => adventuresPanel = value; }
    public GameObject RaidsPanel { get => raidsPanel; set => raidsPanel = value; }
    public GameObject BlackPanel { get => blackPanel; set => blackPanel = value; }
    public GameObject ObjectivesPanel { get => objectivesPanel; set => objectivesPanel = value; }
    public GameObject OptionsPanel { get => optionsPanel; set => optionsPanel = value; }
    public GameObject CreditsPanel { get => creditsPanel; set => creditsPanel = value; }

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        messageBttnPrefab = Resources.Load<GameObject>("Prefabs/UI/Buttons/Message Button");

        heartsPanel = canvas.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject;

        navigationPanel = canvas.transform.GetChild(0).transform.GetChild(1).gameObject;
        objectivesPanel = canvas.transform.GetChild(0).transform.GetChild(2).gameObject;
        startGameBttnPanel = canvas.transform.GetChild(0).transform.GetChild(3).gameObject;

        newMessagePanel = canvas.transform.GetChild(1).gameObject;
        messagePanel = canvas.transform.GetChild(2).gameObject;
        improvementsPanel = canvas.transform.GetChild(3).gameObject;
        adventuresPanel = canvas.transform.GetChild(4).gameObject;
        raidsPanel = canvas.transform.GetChild(5).gameObject;
        optionsPanel = canvas.transform.GetChild(6).gameObject;
        creditsPanel = canvas.transform.GetChild(7).gameObject;
        blackPanel = canvas.transform.GetChild(9).gameObject;
    }
    void Start()
    {
        CheckPlayerPrefs();
        CheckActivePanels();
        CheckHearts();
        CheckHints();
        CheckLanguage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            Level = PlayerPrefs.GetInt("Level");
        }
        else
        {
            level = 1;
        }

        if (!PlayerPrefs.HasKey("Hearts"))
        {
            PlayerPrefs.SetInt("Hearts", 3);
            PlayerPrefs.Save();
        }
    }

    void CheckHearts()
    {
        if (PlayerPrefs.GetInt("Hearts") < 9)
        {
            currentHeartsText.text = PlayerPrefs.GetInt("Hearts").ToString();
        }
        else
        {
            currentHeartsText.text = ">9";
        }
    }

    void CheckHints()
    {
        if (PlayerPrefs.GetInt("Hints") < 9)
        {
            currentHintsText.text = PlayerPrefs.GetInt("Hints").ToString();
        }
        else
        {
            currentHintsText.text = ">9";
        }
    }

    void CheckActivePanels()
    {
        if (level == 1)
        {
            NavigationPanel.SetActive(false);
            ObjectivesPanel.SetActive(false);
            startGameBttnPanel.SetActive(false);
            ShowMessageEffect();
        }
        else if (level == 11
            || level == 15)
        {
            //проверяем, не прочитано ли уже это сообщение
            if (PlayerPrefs.GetInt("Level Messages") != level)
            {
                ShowMessageEffect();
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("Show Message 6")
                || PlayerPrefs.HasKey("Show Message 7")
                || PlayerPrefs.HasKey("Show Message 5"))
            {
                ShowMessageEffect();
            }
        }

        if (level < 11)
        {
            //скрываем улучшения
            NavigationPanel.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (level < 15)
        {
            //скрываем приключения
            NavigationPanel.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Ship Improvements") < 6)
        {
            //скрываем рейды
            NavigationPanel.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void CheckLanguage()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);

        currentLevelText.text = UIelement.UIelements[0].text + Level;
        StartBttnText.text = UIelement.UIelements[1].text;
        currentCoinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void ShowMessageEffect()
    {
        GameObject message = Instantiate(messageBttnPrefab);
        message.transform.SetParent(newMessagePanel.transform, false);
        message.GetComponent<Animation>().Play("Attention");
        newMessagePanel.SetActive(true);
        newMessagePanel.GetComponent<Animation>().Play("Enlarge Panel");
    }

}
