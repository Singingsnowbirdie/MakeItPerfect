using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameByText;
    [SerializeField] TextMeshProUGUI gamedevText;
    [SerializeField] TextMeshProUGUI name_1_Text;
    [SerializeField] TextMeshProUGUI name_2_Text;
    [SerializeField] TextMeshProUGUI artistsHeader;
    [SerializeField] TextMeshProUGUI fontText;
    [SerializeField] TextMeshProUGUI exitBttnText;

    TextAsset asset;
    XMLSettings UIelement;
    AudioSource clickSound;
    MenuController menu;
    GameObject blackPanel;

    void Awake()
    {
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        menu = FindObjectOfType<MenuController>();
        blackPanel = menu.BlackPanel;
    }
    void Start()
    {
        CheckLanguage();
    }

    public void FreepikBttn()
    {
        Application.OpenURL("http://www.freepik.com");
    }
    public void JanGernerBttn()
    {
        Application.OpenURL("http://www.yanone.de");
    }

    public void CheckLanguage()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/Credits");
        UIelement = XMLSettings.Load(asset);

        gameByText.text = UIelement.UIelements[0].text;
        gamedevText.text = UIelement.UIelements[1].text;
        name_1_Text.text = UIelement.UIelements[2].text;
        name_2_Text.text = UIelement.UIelements[3].text;
        artistsHeader.text = UIelement.UIelements[4].text;
        fontText.text = UIelement.UIelements[5].text;
        exitBttnText.text = UIelement.UIelements[6].text;
    }

    public void ExitBttn()
    {
        clickSound.Play();
        StartCoroutine(ClosePanel());
    }

    IEnumerator ClosePanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

}
