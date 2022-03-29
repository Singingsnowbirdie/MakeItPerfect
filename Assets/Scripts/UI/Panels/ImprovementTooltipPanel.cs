using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementTooltipPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tooltipText;

    TextAsset asset;
    XMLSettings UIelement;
    ImprovementsPanel improvementsPanel;
    GameObject blackPanel;
    AudioSource clickSound;
    int currentImprovement;

    public int CurrentImprovement { get => currentImprovement; set => currentImprovement = value; }

    private void Awake()
    {
        improvementsPanel = FindObjectOfType<ImprovementsPanel>();
        blackPanel = improvementsPanel.BlackPanel;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    public void ShowContent()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/Improvements");
        UIelement = XMLSettings.Load(asset);

        if (currentImprovement < 6)
        {
            tooltipText.text = UIelement.UIelements[22].text;
        }
        else if (currentImprovement > 5 && currentImprovement < 9)
        {
            tooltipText.text = UIelement.UIelements[23].text;
        }
        else if (currentImprovement == 9)
        {
            tooltipText.text = UIelement.UIelements[24].text;
        }
        else if (currentImprovement > 9 && currentImprovement < 16)
        {
            tooltipText.text = UIelement.UIelements[currentImprovement + 22].text;
        }
    }

    public void ExitTooltipBttn()
    {
        clickSound.Play();
        gameObject.SetActive(false);
    }
}
