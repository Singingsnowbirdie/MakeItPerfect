using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionsPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI bttnText;
    [SerializeField] TextMeshProUGUI instructionsText;

    TextAsset asset;
    XMLSettings UIelement;
    AudioSource clickSound;

    private void Awake()
    {
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    void Start()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);

        bttnText.text = UIelement.UIelements[12].text;
        instructionsText.text = UIelement.UIelements[28].text;
    }

    public void CloseBttn()
    {
        clickSound.Play();
        gameObject.SetActive(false);
    }
}
