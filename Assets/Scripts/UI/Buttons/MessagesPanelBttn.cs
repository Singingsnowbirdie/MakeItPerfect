using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MessagesPanelBttn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bttnText;

    TextAsset asset;
    XMLSettings UIelement;

    MenuController menu;
    GameObject blackPanel;
    GameObject messPanel;
    GameObject improvementsPanel;
    GameObject adventuresPanel;
    GameObject raidsPanel;

    int level;
    int action;

    AudioSource clickSound;

    public int Action { get => action; set => action = value; }

    private void Awake()
    {
        menu = FindObjectOfType<MenuController>();
        blackPanel = menu.BlackPanel;
        messPanel = menu.MessagePanel;
        improvementsPanel = menu.ImprovementsPanel;
        adventuresPanel = menu.AdventuresPanel;
        raidsPanel = menu.RaidsPanel;
        level = menu.Level;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    void Start()
    {
        ShowText();
    }

    public void ShowText()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);

        if (level == 1
            || level == 3)
        {
            bttnText.text = UIelement.UIelements[19].text;
        }
        else
        {
            bttnText.text = UIelement.UIelements[1].text;
        }
    }

    public void Press()
    {
        clickSound.Play();
        StartCoroutine(CloseMessPanel());
    }

    IEnumerator CloseMessPanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);

        if (Action == 1)
        {
            SceneManager.LoadScene(1);
        }
        else if (Action == 2)
        {
            improvementsPanel.SetActive(true);
            messPanel.SetActive(false);
        }
        else if (Action == 3)
        {
            adventuresPanel.SetActive(true);
            messPanel.SetActive(false);
        }
        else if (Action == 5)
        {
            PlayerPrefs.DeleteKey("Show Message 6");
            PlayerPrefs.SetInt("Show Message 7", 1);
            PlayerPrefs.Save();
            raidsPanel.SetActive(true);
            messPanel.SetActive(false);
        }
        else if (Action == 6)
        {
            PlayerPrefs.DeleteKey("Show Message 7");
            PlayerPrefs.Save();
            improvementsPanel.SetActive(true);
            messPanel.SetActive(false);
        }
        else
        {
            messPanel.SetActive(false);
        }
    }


}
