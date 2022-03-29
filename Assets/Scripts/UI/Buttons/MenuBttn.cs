using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBttn : MonoBehaviour
{
    MenuController menu;

    GameObject improvementsPanel;
    GameObject adventuresPanel;
    GameObject raidsPanel;
    GameObject blackPanel;
    GameObject messagePanel;
    GameObject rewardsPanel;
    GameObject optionsPanel;

    AudioSource clickSound;
    AudioSource letterSound;

    enum Panel
    {
        improvements,
        adventures,
        raids,
        message,
        game,
        options
    }

    [SerializeField] Panel currentPanel;

    void Start()
    {
        menu = FindObjectOfType<MenuController>();
        improvementsPanel = menu.ImprovementsPanel;
        adventuresPanel = menu.AdventuresPanel;
        raidsPanel = menu.RaidsPanel;
        messagePanel = menu.MessagePanel;
        rewardsPanel = menu.RewardsPanel;
        blackPanel = menu.BlackPanel;
        optionsPanel = menu.OptionsPanel;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        letterSound = GameObject.FindGameObjectWithTag("Card").GetComponent<AudioSource>();
    }

    public void Press()
    {
        StartCoroutine(OpenPanel());
        if (currentPanel == Panel.message)
        {
            letterSound.Play();
        }
        else
        {
            clickSound.Play();
        }
    }

    IEnumerator OpenPanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);

        if (currentPanel == Panel.game)
        {
            SceneManager.LoadScene(1);
        }
        else if (currentPanel == Panel.improvements)
        {
            improvementsPanel.SetActive(true);
            improvementsPanel.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (currentPanel == Panel.adventures)
        {
            adventuresPanel.SetActive(true);
        }
        else if (currentPanel == Panel.raids)
        {
            raidsPanel.SetActive(true);
            raidsPanel.GetComponent<RaidsPanel>().CheckTimer();
        }
        else if (currentPanel == Panel.message)
        {
            rewardsPanel.SetActive(false);
            messagePanel.SetActive(true);
        }
        else if (currentPanel == Panel.options)
        {
            rewardsPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }
    }
}
