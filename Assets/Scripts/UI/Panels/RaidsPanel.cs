using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RaidsPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firstRaidMessageText;
    [SerializeField] TextMeshProUGUI secondRaidMessageText;
    [SerializeField] TextMeshProUGUI lootText;
    [SerializeField] TextMeshProUGUI lootCountText;
    [SerializeField] TextMeshProUGUI extraHintsCountText;
    [SerializeField] TextMeshProUGUI extraHeartsCountText;

    [SerializeField] TextMeshProUGUI regularBttnText;
    [SerializeField] TextMeshProUGUI takeLootBttnText;
    [SerializeField] TextMeshProUGUI doubleLootBttnText;
    [SerializeField] TextMeshProUGUI fixForAdsBttnText;
    [SerializeField] TextMeshProUGUI fixForCoinsBttnText;
    [SerializeField] TextMeshProUGUI exitBttnText;

    TextAsset asset;
    XMLSettings UIelement;

    MenuController menu;
    GameObject blackPanel;
    GameObject regularBttnPanel;
    GameObject timerPanel;
    GameObject lootPanel;
    GameObject extraLootPanel;
    GameObject extraHintsPanel;
    GameObject extraHeartsPanel;
    GameObject fix4AdsBttnPanel;
    GameObject fix4CoinsBttnPanel;
    GameObject raidImage;

    DateTime currentDate;
    DateTime newRaidDate;
    DateTime turnDate;

    int loot;
    int raidBttn;
    int shipImprovements;

    bool turnTimer;
    bool raidTimer;

    AudioSource clickSound;

    private void Awake()
    {
        menu = FindObjectOfType<MenuController>();
        blackPanel = menu.BlackPanel;
        raidImage = transform.GetChild(0).transform.GetChild(0).gameObject;
        timerPanel = transform.GetChild(2).gameObject;
        regularBttnPanel = transform.GetChild(3).gameObject;
        fix4CoinsBttnPanel = transform.GetChild(4).gameObject;
        fix4AdsBttnPanel = transform.GetChild(5).gameObject;
        lootPanel = transform.GetChild(7).gameObject;
        extraLootPanel = lootPanel.transform.GetChild(2).gameObject;
        extraHintsPanel = extraLootPanel.transform.GetChild(0).gameObject;
        extraHeartsPanel = extraLootPanel.transform.GetChild(1).gameObject;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    void Start()
    {
        shipImprovements = PlayerPrefs.GetInt("Ship Improvements");
        CheckTimer();
    }

    private void Update()
    {
        if (turnTimer)
            //таймер возвращения из рейда
        {
            currentDate = DateTime.UtcNow.ToLocalTime();
            turnDate = DateTime.Parse(PlayerPrefs.GetString("Raid Date")).AddMinutes(1f);
            TimeSpan turnTimeRemaining = turnDate - currentDate;
            secondRaidMessageText.text = turnTimeRemaining.Minutes + " : " + turnTimeRemaining.Seconds;
            CheckTimer();
        }
        if (raidTimer)
            //таймер отправления в новый рейд
        {
            currentDate = DateTime.UtcNow.ToLocalTime();
            if (PlayerPrefs.HasKey("Boatswain"))
                //если есть боцман
            {
                newRaidDate = DateTime.Parse(PlayerPrefs.GetString("Raid Date")).AddHours(12);
            }
            else
            //если нет боцмана
            {
                newRaidDate = DateTime.Parse(PlayerPrefs.GetString("Raid Date")).AddHours(24);
            }
            TimeSpan raidTimeRemaining = newRaidDate - currentDate;
            secondRaidMessageText.text = raidTimeRemaining.Hours + " : " + raidTimeRemaining.Minutes + " : " + raidTimeRemaining.Seconds;
            CheckTimer();
        }
    }

    public void CheckTimer()
    {
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/Raids");
        UIelement = XMLSettings.Load(asset);
        exitBttnText.text = UIelement.UIelements[1].text;

        if (PlayerPrefs.HasKey("Raid Date"))
        //если есть запись о прошлом рейде
        {
            currentDate = DateTime.UtcNow.ToLocalTime();
            turnDate = DateTime.Parse(PlayerPrefs.GetString("Raid Date")).AddMinutes(1f);
            if (currentDate > turnDate)
            //если корабль уже вернулся
            {
                turnTimer = false;
                if (PlayerPrefs.HasKey("Loot"))
                //если еще не забирали добычу
                {
                    firstRaidMessageText.text = UIelement.UIelements[8].text;

                    if (!PlayerPrefs.HasKey("Damage"))
                    //если еще не рассчитывали повреждения
                    {
                        int damage = Random.Range(10, 31);
                        Debug.Log("damage = " + damage);
                        int damageMod = 0;
                        Debug.Log("damageMod = " + damageMod);

                        if (PlayerPrefs.HasKey("Lookout"))
                        //если нанят впередсмотрящий
                        {
                            Debug.Log("Lookout");
                            damageMod += 10;
                            Debug.Log("damageMod = " + damageMod);
                        }

                        if (shipImprovements > 6)
                        //если улучшали корабль
                        {
                            if (shipImprovements == 7)
                            //если установили 1 пушку
                            {
                                Debug.Log("shipImprovements == 7");
                                damageMod += 10;
                                Debug.Log("damageMod = " + damageMod);
                            }
                            else if (shipImprovements == 8)
                            //если установили 2 пушки
                            {
                                Debug.Log("shipImprovements == 8");
                                damageMod += 20;
                                Debug.Log("damageMod = " + damageMod);
                            }
                            else if (shipImprovements == 9)
                            //если установили 3 пушки
                            {
                                Debug.Log("shipImprovements == 9");
                                damageMod += 30;
                                Debug.Log("damageMod = " + damageMod);
                            }
                        }

                        if (damageMod != 0)
                        //если есть модификатор урона
                        {
                            damageMod = damage * damageMod / 100;
                            damage -= damageMod;
                            Debug.Log("damage = " + damage);
                        }

                        if (shipImprovements == 10)
                        //если есть роджер
                        {
                            int chance = Random.Range(1, 11);
                            if (chance == 11)
                            //если игроку повезло роджер сработал
                            {
                                Debug.Log("Roger");
                                damage = 0;
                            }
                        }
                        PlayerPrefs.SetInt("Damage", damage);
                        PlayerPrefs.Save();
                    }
                    timerPanel.SetActive(true);
                    secondRaidMessageText.text = UIelement.UIelements[9].text + PlayerPrefs.GetInt("Damage").ToString() + "%";
                    regularBttnPanel.SetActive(true);
                    regularBttnText.text = UIelement.UIelements[2].text;
                    raidBttn = 2;
                }
                else
                //если уже забирали добычу
                {
                    if (PlayerPrefs.HasKey("Boatswain"))
                    //если боцман нанят
                    {
                        newRaidDate = DateTime.Parse(PlayerPrefs.GetString("Raid Date")).AddHours(12);
                    }
                    else
                    //если боцман не нанят
                    {
                        newRaidDate = DateTime.Parse(PlayerPrefs.GetString("Raid Date")).AddHours(24);
                    }

                    if (currentDate < newRaidDate)
                    //если дата нового рейда еще не наступила
                    {
                        regularBttnPanel.SetActive(false);
                        raidImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Raids/001");
                        firstRaidMessageText.text = UIelement.UIelements[7].text;
                        timerPanel.SetActive(true);
                        raidTimer = true;
                    }
                    else
                    //если можно отправляться в новый рейд
                    {
                        raidTimer = false;
                        raidImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Raids/000");

                        if (PlayerPrefs.HasKey("Damage"))
                        //если еще не чинили корабль
                        {
                            if (PlayerPrefs.GetInt("Damage") > 0)
                            //если есть повреждения
                            {
                                firstRaidMessageText.text = UIelement.UIelements[9].text + PlayerPrefs.GetInt("Damage").ToString() + "%";
                                secondRaidMessageText.text = UIelement.UIelements[11].text;
                                regularBttnPanel.SetActive(false);
                                fix4AdsBttnPanel.SetActive(true);
                                fix4CoinsBttnPanel.SetActive(true);
                                fixForAdsBttnText.text = UIelement.UIelements[4].text;
                                fixForCoinsBttnText.text = UIelement.UIelements[4].text + PlayerPrefs.GetInt("Damage").ToString();
                            }
                            else
                            //если нет повреждений
                            {
                                if (PlayerPrefs.HasKey("Damage"))
                                {
                                    PlayerPrefs.DeleteKey("Damage");
                                    PlayerPrefs.Save();
                                }
                                firstRaidMessageText.text = UIelement.UIelements[6].text;
                                regularBttnText.text = UIelement.UIelements[0].text;
                                timerPanel.SetActive(false);
                                regularBttnPanel.SetActive(true);
                                raidBttn = 1;
                            }
                        }
                        else
                        //если корабль уже починен
                        {
                            firstRaidMessageText.text = UIelement.UIelements[6].text;
                            regularBttnText.text = UIelement.UIelements[0].text;
                            timerPanel.SetActive(false);
                            regularBttnPanel.SetActive(true);
                            raidBttn = 1;
                        }
                    }
                }
            }
            else
            //если корабль еще не вернулся
            {
                raidImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Raids/002");
                firstRaidMessageText.text = UIelement.UIelements[5].text;
                timerPanel.SetActive(true);
                regularBttnPanel.SetActive(false);
                turnTimer = true;
            }
        }
        else
        //если нет записи о прошлом рейде
        {
            Debug.Log("нет записи о прошлом рейде");
            firstRaidMessageText.text = UIelement.UIelements[6].text;
            regularBttnText.text = UIelement.UIelements[0].text;
            timerPanel.SetActive(false);
            regularBttnPanel.SetActive(true);
            raidBttn = 1;
        }
    }

    public void RaidBttn()
    {
        clickSound.Play();
        StartCoroutine(PressRaidBttn());
    }

    public void TakeLootBttn()
    {
        clickSound.Play();
        StartCoroutine(TakeLoot());
    }

    public void Fix4CoinsBttn()
    {
        clickSound.Play();
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - PlayerPrefs.GetInt("Damage"));
        StartCoroutine(Fix());
    }

    public void ExitBttn()
    {
        clickSound.Play();
        StartCoroutine(ClosePanel());
    }

    public IEnumerator Fix()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        PlayerPrefs.DeleteKey("Damage");
        PlayerPrefs.Save();
        menu.CheckPlayerPrefs();
        menu.CheckLanguage();
        firstRaidMessageText.text = UIelement.UIelements[6].text;
        regularBttnText.text = UIelement.UIelements[0].text;
        timerPanel.SetActive(false);
        raidBttn = 1;
        fix4AdsBttnPanel.SetActive(false);
        fix4CoinsBttnPanel.SetActive(false);
        regularBttnPanel.SetActive(true);
    }

    IEnumerator TakeLoot()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + PlayerPrefs.GetInt("Loot"));
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
        lootPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator ClosePanel()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        if (lootPanel.activeSelf)
        {
            lootPanel.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    IEnumerator PressRaidBttn()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animation>().Play("Dark");
        yield return new WaitForSeconds(1f);
        if (raidBttn == 1)
        //начинаем рейд
        {
            StartRaid();
        }
        else if (raidBttn == 2)
        {
            //получаем награду за рейд
            ShowLoot();
        }
    }
    public void StartRaid()
    {
        PlayerPrefs.SetString("Raid Date", DateTime.UtcNow.ToLocalTime().ToString());
        PlayerPrefs.SetInt("Loot", 0);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }

    public void ShowLoot()
    {
        lootPanel.SetActive(true);
        takeLootBttnText.text = UIelement.UIelements[2].text;
        doubleLootBttnText.text = UIelement.UIelements[3].text;

        if (PlayerPrefs.GetInt("Loot") == 0)
        //если добыча еще не рассчитана
        {
            loot = Random.Range(15, 46);
            Debug.Log("loot =" + loot);
            if (PlayerPrefs.HasKey("Cutthroat"))
            //если нанята рубака
            {
                loot += (int)(loot * 0.1f);
                Debug.Log("Cutthroat loot =" + loot);
            }
            if (PlayerPrefs.HasKey("Thug"))
            //если нанят головорез
            {
                loot += (int)(loot * 0.1f);
                Debug.Log("Thug loot =" + loot);
            }
            PlayerPrefs.SetInt("Loot", loot);
            if (PlayerPrefs.HasKey("Parrot"))
            //если куплен попугай
            {
                PlayerPrefs.SetInt("Extra Hints", Random.Range(1, 4));
            }
            if (PlayerPrefs.HasKey("Quartermaster"))
            //если нанят квартермейстер
            {
                PlayerPrefs.SetInt("Extra Hearts", Random.Range(1, 4));
            }
            PlayerPrefs.Save();
        }
        lootText.text = UIelement.UIelements[10].text;
        lootCountText.text = PlayerPrefs.GetInt("Loot").ToString();
        if (PlayerPrefs.HasKey("Parrot"))
        //если куплен попугай
        {
            extraLootPanel.SetActive(true);
            extraHintsPanel.SetActive(true);
            extraHintsCountText.text = PlayerPrefs.GetInt("Extra Hints").ToString();
        }
        if (PlayerPrefs.HasKey("Quartermaster"))
        //если нанят квартермейстер
        {
            extraLootPanel.SetActive(true);
            extraHeartsPanel.SetActive(true);
            extraHeartsCountText.text = PlayerPrefs.GetInt("Extra Hearts").ToString();
        }
    }
}
