using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperLetter : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI exitBttnText;

    TextAsset asset;
    XMLSettings UIelement;
    AudioSource clickSound;
    GameManager game;
    GameObject blackPanel;

    private void Awake()
    {
        game = FindObjectOfType<GameManager>();
        blackPanel = game.BlackPanel;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }


    void Start()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);

        messageText.text = UIelement.UIelements[22].text;
        exitBttnText.text = UIelement.UIelements[21].text;
        game.HintsBttn.SetActive(false);
        game.Canvas.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        game.Canvas.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ExitBttn()
    {
        clickSound.Play();
        StartCoroutine(ExitToMenu());
    }

    IEnumerator ExitToMenu()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
SceneManager.LoadScene(0);
    }

}
