using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;

    GameObject imagePanel;

    TextAsset asset;
    XMLSettings UIelement;

    MenuController menu;
    ObjectivesPanel objectivesPanel;
    MessagesPanelBttn bttn;

    [SerializeField] int level;

    private void Awake()
    {
        menu = FindObjectOfType<MenuController>();
        level = menu.Level;
        imagePanel = transform.GetChild(0).gameObject;
        objectivesPanel = FindObjectOfType<ObjectivesPanel>();
        bttn = FindObjectOfType<MessagesPanelBttn>();
    }

    void Start()
    {
        ShowMessage();
    }

    public void ShowMessage()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/Messages");
        UIelement = XMLSettings.Load(asset);

        if (level == 1)
        {
            //знакомим с игрой
            messageText.text = UIelement.UIelements[0].text;
            GameObject imagePref = Resources.Load<GameObject>("Prefabs/Messages/Message 1");
            GameObject image = Instantiate(imagePref);
            image.transform.SetParent(imagePanel.transform, false);
            PlayerPrefs.SetInt("Level Messages", level);
            bttn.Action = 1;
        }
        else if (level == 11)
        {
            //завершение первой цели
            //рассказываем, что на вырученные деньги можно улучшать корабль
            messageText.text = UIelement.UIelements[1].text;
            GameObject imagePref = Resources.Load<GameObject>("Prefabs/Messages/Message 2");
            GameObject image = Instantiate(imagePref);
            image.transform.SetParent(imagePanel.transform, false);
            PlayerPrefs.SetInt("Level Messages", level);
            bttn.Action = 2;
        }
        else if (level == 15)
        {
            //открываем приключения
            messageText.text = UIelement.UIelements[2].text;
            GameObject imagePref = Resources.Load<GameObject>("Prefabs/Messages/Message 4");
            GameObject image = Instantiate(imagePref);
            image.transform.SetParent(imagePanel.transform, false);
            PlayerPrefs.SetInt("Level Messages", level);
            bttn.Action = 3;
        }
        else if (PlayerPrefs.HasKey("Show Message 6"))
        {
            //открываем рейды
            messageText.text = UIelement.UIelements[4].text;
            GameObject imagePref = Resources.Load<GameObject>("Prefabs/Messages/Message 6");
            GameObject image = Instantiate(imagePref);
            image.transform.SetParent(imagePanel.transform, false);
            bttn.Action = 5;
        }
        else if (PlayerPrefs.HasKey("Show Message 7"))
        {
            //начинаем набор команды
            PlayerPrefs.DeleteKey("Show Message 7");
            PlayerPrefs.SetInt("Team", 0);
            PlayerPrefs.Save();
            messageText.text = UIelement.UIelements[5].text;
            GameObject imagePref = Resources.Load<GameObject>("Prefabs/Messages/Message 7");
            GameObject image = Instantiate(imagePref);
            image.transform.SetParent(imagePanel.transform, false);
            objectivesPanel.GetComponent<ObjectivesPanel>().CheckPlayerPrefs();
            bttn.Action = 6;
        }




    }
}
