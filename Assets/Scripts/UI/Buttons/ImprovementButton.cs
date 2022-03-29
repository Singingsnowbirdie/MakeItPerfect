using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImprovementButton : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI improvementText;
    [SerializeField] TextMeshProUGUI improvementCostText;

    TextAsset asset;
    XMLSettings UIelement;

    ImprovementsPanel improvementsPanel;
    GameObject blackPanel;
    GameObject tooltipPanel;
    AudioSource clickSound;

    [SerializeField] int currentImprovement;
    [SerializeField] int currentCost;
    [SerializeField] int coinsCount;

    public int CurrentImprovement { get => currentImprovement; set => currentImprovement = value; }

    public void Start()
    {
        improvementsPanel = FindObjectOfType<ImprovementsPanel>();
        blackPanel = improvementsPanel.BlackPanel;
        tooltipPanel = improvementsPanel.TooltipPanel;
        CheckCurrentImprovement();
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    public void CheckCurrentImprovement()
    {
        coinsCount = PlayerPrefs.GetInt("Coins");

        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/Improvements");
        UIelement = XMLSettings.Load(asset);

        if (currentImprovement < 10)
        {
            currentCost = ImprovementPrice.improvementsCost[currentImprovement];
            improvementText.text = UIelement.UIelements[CurrentImprovement + 12].text;
            improvementCostText.text = currentCost.ToString();
        }
        else if (currentImprovement >= 10)
        {
            improvementsPanel = FindObjectOfType<ImprovementsPanel>();
            if (improvementsPanel.ImprovementType == 0)
            {
                improvementText.text = UIelement.UIelements[3].text;
                improvementCostText.transform.parent.gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            else if (improvementsPanel.ImprovementType == 1)
            {
                if (currentImprovement < 16)
                {
                    currentCost = ImprovementPrice.improvementsCost[currentImprovement];
                    improvementText.text = UIelement.UIelements[CurrentImprovement + 16].text;
                    improvementCostText.text = currentCost.ToString();
                }
                else
                {
                    improvementText.text = UIelement.UIelements[3].text;
                    improvementCostText.transform.parent.gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                    GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }
        }
    }

    public void Press()
    {
        clickSound.Play();
        if (currentCost <= coinsCount)
        {
            if (tooltipPanel.activeSelf)
            {
                tooltipPanel.SetActive(false);
            }
            improvementsPanel.BuyImprovementBttn(currentImprovement);
        }
        else
        {
            improvementText.text = UIelement.UIelements[1].text;
            improvementCostText.transform.parent.gameObject.SetActive(false);
        }
    }

    public void hintBttn()
    {
        clickSound.Play();
        if (!tooltipPanel.activeSelf)
        {
            tooltipPanel.SetActive(true);
            tooltipPanel.GetComponent<ImprovementTooltipPanel>().CurrentImprovement = currentImprovement;
            tooltipPanel.GetComponent<ImprovementTooltipPanel>().ShowContent();
        }
        else
        {
            tooltipPanel.SetActive(false);
        }
    }
}
