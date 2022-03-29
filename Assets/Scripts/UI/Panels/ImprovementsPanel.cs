using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ImprovementsPanel : MonoBehaviour
{
    TextAsset asset;
    XMLSettings UIelement;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] TextMeshProUGUI improvementsCounterText;
    [SerializeField] TextMeshProUGUI exitBttnText;
    [SerializeField] TextMeshProUGUI exitMenuBttnText;
    [SerializeField] TextMeshProUGUI menuBttnText;
    [SerializeField] TextMeshProUGUI coinsCountText;

    [Header("Bttns")]
    [SerializeField] GameObject teamMenuBttn;
    GameObject improvementBttnPref;

    [Header("Team")]
    [SerializeField] GameObject parrotPanel;
    [SerializeField] GameObject quartermasterPanel;
    [SerializeField] GameObject lookoutPanel;
    [SerializeField] GameObject boatswainPanel;
    [SerializeField] GameObject cutthroatPanel;
    [SerializeField] GameObject thugPanel;

    //panels
    GameObject improvementImagePanel;
    GameObject shipPanel;
    GameObject teamPanel;
    GameObject improvementBttnPanel;
    GameObject improvementBttnsContainer;
    GameObject tooltipPanel;
    GameObject improvementsMenuPanel;
    GameObject blackPanel;

    //other
    MenuController menu;
    ObjectivesPanel objectives;
    GameObject oldElement;
    GameObject newElement;
    AudioSource clickSound;
    AudioSource doubloonsSound;

    int improvementType;
    int shipImprovements;
    int teamMates;
    int coinsCount;

    bool team;

    public int CoinsCount { get => coinsCount; set => coinsCount = value; }
    public GameObject BlackPanel { get => blackPanel; set => blackPanel = value; }
    public GameObject TooltipPanel { get => tooltipPanel; set => tooltipPanel = value; }
    public int ImprovementType { get => improvementType; set => improvementType = value; }

    private void Awake()
    {
        menu = FindObjectOfType<MenuController>();
        objectives = FindObjectOfType<ObjectivesPanel>();

        BlackPanel = menu.BlackPanel;
        improvementImagePanel = transform.GetChild(1).gameObject;
        shipPanel = transform.GetChild(1).transform.GetChild(0).gameObject;
        teamPanel = transform.GetChild(1).transform.GetChild(1).gameObject;
        improvementBttnsContainer = transform.GetChild(2).gameObject;
        improvementBttnPanel = transform.GetChild(2).transform.GetChild(1).gameObject;
        tooltipPanel = transform.GetChild(4).gameObject;
        improvementsMenuPanel = transform.GetChild(5).gameObject;

        ImprovementType = 0;
        improvementBttnPref = Resources.Load<GameObject>("Prefabs/UI/Buttons/Improvement Button");

        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        doubloonsSound = GameObject.FindGameObjectWithTag("Doubloons").GetComponent<AudioSource>();
    }

    void Start()
    {
        CheckPlayerPrefs();
        ShowImprovementType();
    }

    public void CheckPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Ship Improvements"))
        {
            PlayerPrefs.SetInt("Ship Improvements", 0);
            PlayerPrefs.Save();
        }
        shipImprovements = PlayerPrefs.GetInt("Ship Improvements");
        CoinsCount = PlayerPrefs.GetInt("Coins");
        coinsCountText.text = CoinsCount.ToString();

        if (PlayerPrefs.HasKey("Team"))
        {
            team = true;
            teamMates = PlayerPrefs.GetInt("Team");
            teamMenuBttn.GetComponent<CanvasGroup>().blocksRaycasts = true;
            teamMenuBttn.transform.GetChild(0).gameObject.SetActive(false);
            teamMenuBttn.transform.GetChild(1).gameObject.SetActive(true);
            teamMenuBttn.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void ShowShip()
    {
        for (int i = 0; i < improvementBttnPanel.transform.childCount; i++)
        {
            Destroy(improvementBttnPanel.transform.GetChild(i).gameObject);
        }

        improvementBttnsContainer.transform.GetChild(0).gameObject.SetActive(false);

        for (int i = 0; i < shipPanel.transform.childCount; i++)
        {
            if (i > 0 && i < 9)
            {
                shipPanel.transform.GetChild(i).gameObject.SetActive(false);
                shipPanel.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (i == 9)
            {
                shipPanel.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        if (shipImprovements > 9)
        {
            shipPanel.transform.GetChild(shipImprovements - 1).gameObject.SetActive(true);
        }
        shipPanel.transform.GetChild(shipImprovements).gameObject.SetActive(true);

        GameObject bttn = Instantiate(improvementBttnPref);
        bttn.transform.SetParent(improvementBttnPanel.transform, false);
        bttn.GetComponent<ImprovementButton>().CurrentImprovement = shipImprovements;
        bttn.GetComponent<ImprovementButton>().CheckCurrentImprovement();
    }
    public void ShowTeam()
    {
        for (int i = 0; i < improvementBttnPanel.transform.childCount; i++)
        {
            Destroy(improvementBttnPanel.transform.GetChild(i).gameObject);
        }

        if (PlayerPrefs.HasKey("Lookout"))
        {
            lookoutPanel.SetActive(true);
            lookoutPanel.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject bttn = Instantiate(improvementBttnPref);
            bttn.transform.SetParent(improvementBttnPanel.transform, false);
            bttn.GetComponent<ImprovementButton>().CurrentImprovement = 10;
        }

        if (PlayerPrefs.HasKey("Cutthroat"))
        {
            cutthroatPanel.SetActive(true);
            cutthroatPanel.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject bttn = Instantiate(improvementBttnPref);
            bttn.transform.SetParent(improvementBttnPanel.transform, false);
            bttn.GetComponent<ImprovementButton>().CurrentImprovement = 11;
        }

        if (PlayerPrefs.HasKey("Thug"))
        {
            thugPanel.SetActive(true);
            thugPanel.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject bttn = Instantiate(improvementBttnPref);
            bttn.transform.SetParent(improvementBttnPanel.transform, false);
            bttn.GetComponent<ImprovementButton>().CurrentImprovement = 12;
        }

        if (PlayerPrefs.HasKey("Boatswain"))
        {
            boatswainPanel.SetActive(true);
            boatswainPanel.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject bttn = Instantiate(improvementBttnPref);
            bttn.transform.SetParent(improvementBttnPanel.transform, false);
            bttn.GetComponent<ImprovementButton>().CurrentImprovement = 13;
        }

        if (PlayerPrefs.HasKey("Quartermaster"))
        {
            quartermasterPanel.SetActive(true);
            quartermasterPanel.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject bttn = Instantiate(improvementBttnPref);
            bttn.transform.SetParent(improvementBttnPanel.transform, false);
            bttn.GetComponent<ImprovementButton>().CurrentImprovement = 14;
        }

        if (PlayerPrefs.HasKey("Parrot"))
        {
            parrotPanel.SetActive(true);
            parrotPanel.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            GameObject bttn = Instantiate(improvementBttnPref);
            bttn.transform.SetParent(improvementBttnPanel.transform, false);
            bttn.GetComponent<ImprovementButton>().CurrentImprovement = 15;
        }

        if (teamMates < 5)
        {
            improvementBttnsContainer.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            improvementBttnsContainer.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (teamMates == 6)
        {
            GameObject bttn = Instantiate(improvementBttnPref);
            bttn.transform.SetParent(improvementBttnPanel.transform, false);
            bttn.GetComponent<ImprovementButton>().CurrentImprovement = 16;
            improvementBttnsContainer.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void ShowImprovementType()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/Improvements");
        UIelement = XMLSettings.Load(asset);

        if (ImprovementType == 0)
        {
            headerText.text = UIelement.UIelements[11].text;
            improvementsCounterText.text = UIelement.UIelements[0].text + shipImprovements.ToString() + "/10";
            shipPanel.SetActive(true);
            teamPanel.SetActive(false);
            ShowShip();
        }
        else if (ImprovementType == 1)
        {
            headerText.text = UIelement.UIelements[25].text;
            improvementsCounterText.text = UIelement.UIelements[7].text + teamMates.ToString() + "/6";
            shipPanel.SetActive(false);
            teamPanel.SetActive(true);
            ShowTeam();
        }
        exitBttnText.text = UIelement.UIelements[2].text;
        exitMenuBttnText.text = UIelement.UIelements[2].text;
        menuBttnText.text = UIelement.UIelements[8].text;
    }

    public void ExitBttn()
    {
        clickSound.Play();
        StartCoroutine(ClosePanel());
    }

    public void ImprovementsMenuBttn()
    {
        clickSound.Play();
        StartCoroutine(OpenImprovementsMenu());
    }

    IEnumerator ClosePanel()
    {
        BlackPanel.SetActive(true);
        BlackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    IEnumerator OpenImprovementsMenu()
    {
        BlackPanel.SetActive(true);
        BlackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        improvementsMenuPanel.SetActive(true);
    }

    public void BuyImprovementBttn(int ID)
    {
        doubloonsSound.Play();
        StartCoroutine(ApplyImprovement(ID));
    }

    IEnumerator ApplyImprovement(int ID)
    {
        CanvasGroup[] bttns = FindObjectsOfType<CanvasGroup>();

        foreach (CanvasGroup bttn in bttns)
        {
            bttn.blocksRaycasts = false;
        }

        if (improvementType == 0)
        {
            ImproveShip(ID);
        }
        else if (improvementType == 1)
        {
            ImproveTeam(ID);
        }
        yield return new WaitForSeconds(1f);
        CheckPlayerPrefs();
        improvementsMenuPanel.GetComponent<ImprovementsMenuPanel>().CheckCounters();
        ShowImprovementType();
        menu.CheckPlayerPrefs();
        menu.CheckLanguage();
        objectives.CheckPlayerPrefs();
        objectives.CheckProgress();

        foreach (CanvasGroup bttn in bttns)
        {
            bttn.blocksRaycasts = true;
        }
    }

    void ImproveShip(int ID)
    {
        oldElement = shipPanel.transform.GetChild(shipImprovements).gameObject;
        newElement = shipPanel.transform.GetChild(shipImprovements + 1).gameObject;
        GameObject effect = newElement.transform.GetChild(0).gameObject;
        if (ID != 9)
        {
            oldElement.GetComponent<Animation>().Play("Dissolution");
        }
        newElement.SetActive(true);
        newElement.GetComponent<Animation>().Play("Appearance");
        effect.SetActive(true);
        effect.GetComponent<ParticleSystem>().Play();
        PlayerPrefs.SetInt("Ship Improvements", PlayerPrefs.GetInt("Ship Improvements") + 1);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - ImprovementPrice.improvementsCost[shipImprovements]);
        if (ID == 5)
        {
            PlayerPrefs.SetInt("Show Message 6", 1);
            menu.NavigationPanel.transform.GetChild(2).gameObject.SetActive(true);
            StartCoroutine(Award_1());
        }
        PlayerPrefs.Save();
    }

    void ImproveTeam(int ID)
    {
        if (ID == 10)
        {
            newElement = lookoutPanel;
            PlayerPrefs.SetInt("Lookout", 1);
        }
        else if (ID == 11)
        {
            newElement = cutthroatPanel;
            PlayerPrefs.SetInt("Cutthroat", 1);
        }
        else if (ID == 12)
        {
            newElement = thugPanel;
            PlayerPrefs.SetInt("Thug", 1);
        }
        else if (ID == 13)
        {
            newElement = boatswainPanel;
            PlayerPrefs.SetInt("Boatswain", 1);
        }
        else if (ID == 14)
        {
            newElement = quartermasterPanel;
            PlayerPrefs.SetInt("Quartermaster", 1);
        }
        else if (ID == 15)
        {
            newElement = parrotPanel;
            PlayerPrefs.SetInt("Parrot", 1);
        }
        GameObject effect = newElement.transform.GetChild(0).gameObject;
        newElement.SetActive(true);
        newElement.GetComponent<Animation>().Play("Appearance");
        effect.SetActive(true);
        effect.GetComponent<ParticleSystem>().Play();
        PlayerPrefs.SetInt("Team", PlayerPrefs.GetInt("Team") + 1);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - ImprovementPrice.improvementsCost[ID]);
        PlayerPrefs.Save();
    }
    public void ArrowBttn()
    {
        clickSound.Play();
        improvementBttnPanel.transform.GetChild(0).transform.SetAsLastSibling();

        if (tooltipPanel.activeSelf)
        {
            tooltipPanel.SetActive(false);
        }
    }
    IEnumerator Award_1()
    {
        yield return new WaitForSeconds(1f);
        BlackPanel.SetActive(true);
        BlackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        menu.ShowMessageEffect();
    }
}
