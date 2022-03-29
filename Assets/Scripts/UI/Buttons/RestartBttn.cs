using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartBttn : MonoBehaviour
{

    GameManager game;
    GameObject blackPanel;

    AudioSource clickSound;
    AudioSource fireSound;

    private void Awake()
    {
        game = FindObjectOfType<GameManager>();
        blackPanel = game.BlackPanel;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        fireSound = GameObject.FindGameObjectWithTag("Fire").GetComponent<AudioSource>();
    }

    public void Press()
    {
        clickSound.Play();
        fireSound.Stop();
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }


}
