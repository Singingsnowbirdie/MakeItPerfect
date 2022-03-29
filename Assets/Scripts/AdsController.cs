using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//вешаем на кнопку, которая показывает рекламу

public class AdsController : MonoBehaviour, IUnityAdsListener
{
    string gameId = "3770057"; // идентификатор приложения
    string myPlacementId = "rewardedVideo"; // идентификатор видео
    Button myButton; // кнопка, которая будет показывать ролик
    Music music;
    AudioSource clickSound;

    enum Type
    {
        level,
        hint,
        fixShip,
        raid,
        heart
    }

    [SerializeField] Type rewardType;

    void Awake()
    {
        music = FindObjectOfType<Music>();
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        //показываем скрипту кнопку, на которой он висит
        myButton = gameObject.GetComponent<Button>();
    }
    void Start()
    {
        if (Advertisement.isSupported)
        //если реклама поддерживается
        {
            //инициализируем ее, указываем ID нашего приложения
            //false/true включает и выключает тест-мод
            Advertisement.Initialize(gameId, false);
            //кнопка вызова рекламы будет активна, только если реклама готова
            myButton.interactable = Advertisement.IsReady(myPlacementId);
            //если кнопку нажали, включаем на ней "слушателя", который следит, просмотрено ли рекламное видео
            if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);
            Advertisement.AddListener(this);
            Debug.Log("AddListener");
        }
    }

    public void ShowRewardedVideo()
    //показываем рекламу
    {
        clickSound.Play();
        Advertisement.Show(myPlacementId);
    }

    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    //когда реклама готова к показу
    {
        if (placementId == myPlacementId)
        //если она отвечает критерию: "видео с вознаграждением"
        {
            //кнопка становится активна
            myButton.interactable = true;
        }
    }

    void IUnityAdsListener.OnUnityAdsDidError(string message)
    //если при показе рекламы произошла ошибка
    {
        // ошибка
    }

    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    // когда запускается реклама
    {
        //выключаем музыку в игре
        music.Pause();
    }

    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    //когда рекламное видео завершено
    {
        //включаем музыку обратно
        music.Play();

        if (showResult == ShowResult.Finished)
        {
            // награждаем пользователя за то, что посмотрел ролик
            if (rewardType == Type.level)
            {
                Debug.Log("LevelReward");
                LevelReward();
            }
            else if (rewardType == Type.hint)
            {
                SellHint();
            }
            else if (rewardType == Type.heart)
            {
                SellHeart();
            }
            else if (rewardType == Type.fixShip)
            {
                FixShip();
            }
            else if (rewardType == Type.raid)
            {
                Loot4Raid();
            }

            myButton.interactable = false;
        }
        else if (showResult == ShowResult.Skipped)
        {
            // не вознаграждайте пользователя за пропуск объявления.
        }
        else if (showResult == ShowResult.Failed)
        {
            // объявление не было завершено из-за ошибки
        }
    }

    public void LevelReward()
    {
        WinnerPanel panel = FindObjectOfType<WinnerPanel>();
        int currentCoins = PlayerPrefs.GetInt("Coins") + (panel.Coins * 2);
        PlayerPrefs.SetInt("Coins", currentCoins);
        PlayerPrefs.Save();
        //Advertisement.RemoveListener(this);
        SceneManager.LoadScene(0);
    }

    public void DonationReward()
    {
        DeveloperLetter letter = FindObjectOfType<DeveloperLetter>();
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 30);
        PlayerPrefs.Save();
        //Advertisement.RemoveListener(this);
        letter.ExitBttn();
    }

    public void SellHint()
    {
        MarketPanel market = FindObjectOfType<MarketPanel>();
        GameManager game = FindObjectOfType<GameManager>();
        PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + 1);
        PlayerPrefs.Save();
        market.ShowCoinsAmount();
        market.ShowHintsAmount();
        game.CheckHintsCount();
        //Advertisement.RemoveListener(this);
    }

    public void SellHeart()
    {
        MarketPanel market = FindObjectOfType<MarketPanel>();
        GameManager game = FindObjectOfType<GameManager>();
        PlayerPrefs.SetInt("Hearts", PlayerPrefs.GetInt("Hearts") + 1);
        PlayerPrefs.Save();
        //Advertisement.RemoveListener(this);
        market.ShowCoinsAmount();
        market.ShowHeartsAmount();
    }

    public void FixShip()
    {
        RaidsPanel fix = FindObjectOfType<RaidsPanel>();
        //Advertisement.RemoveListener(this);
        StartCoroutine(fix.Fix());
    }

    public void Loot4Raid()
    {
        MenuController menu = FindObjectOfType<MenuController>();
        RaidsPanel raids = FindObjectOfType<RaidsPanel>();
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetInt("Loot") * 2);
        PlayerPrefs.DeleteKey("Loot");
        if (PlayerPrefs.HasKey("Extra Hints"))
        {
            PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + PlayerPrefs.GetInt("Extra Hints"));
            PlayerPrefs.DeleteKey("Extra Hints");
        }
        if (PlayerPrefs.HasKey("Extra Hearts"))
        {
            PlayerPrefs.SetInt("Hearts", PlayerPrefs.GetInt("Hearts") + PlayerPrefs.GetInt("Extra Hearts"));
            PlayerPrefs.DeleteKey("Extra Hearts");
        }
        PlayerPrefs.Save();
        menu.CheckPlayerPrefs();
        menu.CheckLanguage();
        //Advertisement.RemoveListener(this);
        raids.ExitBttn();
    }

    public void OnDestroy()
    {
        Debug.Log("RemoveListener");
        Advertisement.RemoveListener(this);
    }


}
