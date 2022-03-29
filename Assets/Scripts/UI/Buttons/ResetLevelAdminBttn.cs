using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelAdminBttn : MonoBehaviour
{
    [SerializeField] Level setLevel;

    enum Level
    {
        zero, ten, twenty, thirty, forty, fifty, sixty
    }

    public void Press()
    {
        PlayerPrefs.DeleteAll();
        if (setLevel == Level.ten)
        {
            PlayerPrefs.SetInt("Level", 10);
            PlayerPrefs.SetInt("Coins", 540);
        }
        else if (setLevel == Level.twenty)
        {
            PlayerPrefs.SetInt("Level", 20);
            PlayerPrefs.SetInt("Coins", 1140);
        }
        else if (setLevel == Level.thirty)
        {
            PlayerPrefs.SetInt("Level", 30);
            PlayerPrefs.SetInt("Coins", 1740);
        }
        else if (setLevel == Level.forty)
        {
            PlayerPrefs.SetInt("Level", 40);
            PlayerPrefs.SetInt("Coins", 2340);
        }
        else if (setLevel == Level.fifty)
        {
            PlayerPrefs.SetInt("Level", 50);
            PlayerPrefs.SetInt("Coins", 2940);
        }
        else if (setLevel == Level.sixty)
        {
            PlayerPrefs.SetInt("Level", 60);
            PlayerPrefs.SetInt("Coins", 3540);
        }
        PlayerPrefs.Save();
    }


}
