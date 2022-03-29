using TMPro;
using UnityEngine;

public class ObjectivesPanel : MonoBehaviour
{
    [Header("Goal 1")]
    GameObject goal_1;
    [SerializeField] TextMeshProUGUI goal_1Text;
    [SerializeField] TextMeshProUGUI goal_1CounterText;

    [Header("Goal 2")]
    GameObject goal_2;
    [SerializeField] TextMeshProUGUI goal_2Text;
    [SerializeField] TextMeshProUGUI goal_2CounterText;

    [Header("Goal 3")]
    GameObject goal_3;
    [SerializeField] TextMeshProUGUI goal_3Text;
    [SerializeField] TextMeshProUGUI goal_3CounterText;

    MenuController menu;

    TextAsset asset;
    XMLSettings UIelement;

    int shipRepairing;
    int teamMates;

    private void Awake()
    {
        goal_1 = transform.GetChild(0).gameObject;
        goal_2 = transform.GetChild(1).gameObject;
        goal_3 = transform.GetChild(2).gameObject;

        menu = FindObjectOfType<MenuController>();
    }

    void Start()
    {
        CheckLanguage();
        CheckPlayerPrefs();
        CheckProgress();
    }

    public void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Ship Improvements"))
        {
            shipRepairing = PlayerPrefs.GetInt("Ship Improvements");
        }

        if (PlayerPrefs.HasKey("Team"))
        {
            goal_3.SetActive(true);
            teamMates = PlayerPrefs.GetInt("Team");
        }
    }

    public void CheckLanguage()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/Objectives");
        UIelement = XMLSettings.Load(asset);

        goal_1Text.text = UIelement.UIelements[0].text;
        goal_2Text.text = UIelement.UIelements[1].text;
        goal_3Text.text = UIelement.UIelements[2].text;
    }

    public void CheckProgress()
    {
        if (menu.Level >= 1)
        {
            if (menu.Level <= 10)
            {
                goal_1CounterText.text = (menu.Level - 1).ToString() + "/10";
            }
            else
            {
                goal_1.transform.GetChild(1).gameObject.SetActive(false);
                goal_1.transform.GetChild(3).gameObject.SetActive(false);
                goal_1.transform.GetChild(2).gameObject.SetActive(true);
                goal_2.SetActive(true);
            }
        }

        if (shipRepairing >= 0 && shipRepairing <= 5)
        {
            goal_2CounterText.text = (shipRepairing).ToString() + "/6";
        }
        else
        {
            goal_2.transform.GetChild(1).gameObject.SetActive(false);
            goal_2.transform.GetChild(3).gameObject.SetActive(false);
            goal_2.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (teamMates >= 0 && teamMates < 4)
        {
            goal_3CounterText.text = (teamMates).ToString() + "/4";
        }
        else
        {
            goal_3.transform.GetChild(1).gameObject.SetActive(false);
            goal_3.transform.GetChild(3).gameObject.SetActive(false);
            goal_3.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
