using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] Type levelType;
    [SerializeField] GameObject[] hints;
    int levelCost;
    int answers;
    string puzzle;
    AudioSource hintSound;
    GameManager game;

    enum Type
    {
        differences,
        mathematical,
        puzzle,
        memoCard
    }

    public int LevelCost { get => levelCost; set => levelCost = value; }
    public int Answers { get => answers; set => answers = value; }
    public string Puzzle { get => puzzle; set => puzzle = value; }

    private void Awake()
    {
        Puzzle = levelType.ToString();

        hintSound = GameObject.FindGameObjectWithTag("Hint").GetComponent<AudioSource>();
        game = FindObjectOfType<GameManager>();
        if (levelType == Type.differences)
        {
            Answers = hints.Length;
            levelCost = hints.Length * 10;
        }
        else if (levelType == Type.mathematical)
        {
            Answers = 1;
            levelCost = 30;
        }
        else if (levelType == Type.puzzle)
        {
            Answers = 1;
            levelCost = 30;
        }
        else if (levelType == Type.memoCard)
        {
            Answers = 1;
            levelCost = 30;
            game.HintsBttn.SetActive(false);
        }
    }

    public void ShowHint()
    {
        if (levelType == Type.differences)
        {
            for (int i = 0; i < hints.Length; i++)
            {
                if (hints[i].transform.parent.GetComponent<State>().Solved == false)
                {
                    hints[i].gameObject.SetActive(true);
                    hintSound.Play();
                    hints[i].GetComponent<Animation>().Play("Hint");
                    PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") - 1);
                    PlayerPrefs.Save();
                    game.CheckHintsCount();
                    break;
                }
            }
        }
        else if (levelType == Type.mathematical
            || levelType == Type.puzzle)
        {
            WrongBttn[] wrongs = FindObjectsOfType<WrongBttn>();
            Debug.Log("Total hints = " + wrongs.Length);
            if (wrongs.Length > 1)
            {
                int i = Random.Range(0, wrongs.Length);
                Debug.Log("Random hint = " + i);
                GameObject wrong = wrongs[i].gameObject.transform.parent.gameObject;
                wrong.transform.GetChild(0).gameObject.SetActive(false);
                wrong.transform.GetChild(1).gameObject.SetActive(false);
                if (levelType == Type.mathematical)
                {
                    wrong.transform.GetChild(2).gameObject.SetActive(false);
                }
                hintSound.Play();
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") - 1);
                PlayerPrefs.Save();
                game.CheckHintsCount();
            }
            else if (wrongs.Length == 1)
            {
                GameObject wrong = wrongs[0].gameObject.transform.parent.gameObject;
                wrong.transform.GetChild(0).gameObject.SetActive(false);
                wrong.transform.GetChild(1).gameObject.SetActive(false);
                if (levelType == Type.mathematical)
                {
                    wrong.transform.GetChild(2).gameObject.SetActive(false);
                }
                hintSound.Play();
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") - 1);
                PlayerPrefs.Save();
                game.CheckHintsCount();
            }
        }
    }
}
