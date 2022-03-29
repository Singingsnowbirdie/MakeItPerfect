using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongBttn : MonoBehaviour
{
    GameManager game;
    LevelController level;
    AudioSource wrongSound;
    GameObject image;

    private void Awake()
    {
        gameObject.AddComponent<CanvasGroup>();
        game = FindObjectOfType<GameManager>();
        wrongSound = GameObject.FindGameObjectWithTag("Wrong").GetComponent<AudioSource>();
        level = FindObjectOfType<LevelController>();
    }

    public void Press()
    {
        wrongSound.Play();
        game.WrongAnswer();
        if (level.Puzzle == "mathematical")
        {
            image = transform.parent.transform.GetChild(2).gameObject;
            image.SetActive(true);
            image.GetComponent<Animation>().Play("Appearance");
            gameObject.SetActive(false);
        }
        else if (level.Puzzle == "puzzle")
        {
            image = transform.parent.transform.GetChild(1).gameObject;
            image.SetActive(true);
            image.GetComponent<Animation>().Play("Appearance");
            gameObject.SetActive(false);
        }
    }


}
