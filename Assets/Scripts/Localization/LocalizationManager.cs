using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    //public static string currentLanguage = "ru_RU";
    public static string currentLanguage = "en_US";

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetString("Language", "en_US");
                PlayerPrefs.Save();
            }
        }
        currentLanguage = PlayerPrefs.GetString("Language");

        Debug.Log(currentLanguage);

    }

    public string CurrentLanguage
    {
        get
        {
            return currentLanguage;
        }
        set
        {
            PlayerPrefs.SetString("Language", value);
            PlayerPrefs.Save();
            currentLanguage = PlayerPrefs.GetString("Language");
        }
    }


}
