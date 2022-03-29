using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImprovementsMenuPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI shipCounterText;
    [SerializeField] TextMeshProUGUI teamCounterText;

    ImprovementsPanel improvements;
    GameObject blackPanel;
    AudioSource clickSound;

    private void Awake()
    {
        improvements = FindObjectOfType<ImprovementsPanel>();
        blackPanel = improvements.BlackPanel;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        CheckCounters();
    }

    public void ShipBttn()
    {
        clickSound.Play();
        improvements.ImprovementType = 0;
        improvements.ShowImprovementType();
        StartCoroutine(ClosePanel());
    }
    public void TeamBttn()
    {
        clickSound.Play();
        improvements.ImprovementType = 1;
        improvements.ShowImprovementType();
        StartCoroutine(ClosePanel());
    }
    public void ExitBttn()
    {
        clickSound.Play();
        improvements.StartCoroutine(CloseEmprovements());
    }

    IEnumerator ClosePanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    IEnumerator CloseEmprovements()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        improvements.gameObject.SetActive(false);
    }

    public void CheckCounters()
    {
        shipCounterText.text = PlayerPrefs.GetInt("Ship Improvements").ToString() + "/10";
        teamCounterText.text = PlayerPrefs.GetInt("Team").ToString() + "/6";
    }




}
