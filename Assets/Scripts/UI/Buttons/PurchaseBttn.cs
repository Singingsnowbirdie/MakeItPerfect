using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseBttn : MonoBehaviour
{

    MarketPanel market;
    GameManager game;
    AudioSource clickSound;
    TextAsset asset;
    XMLSettings UIelement;

    private void Awake()
    {
        market = FindObjectOfType<MarketPanel>();
        game = FindObjectOfType<GameManager>();
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);
    }

    public void Press()
    {
        clickSound.Play();
        if (PlayerPrefs.GetInt("Coins") > market.CoinsToPay)
        {
            if (market.ItemToBuy == 1)
            {
                PlayerPrefs.SetInt("Hints", PlayerPrefs.GetInt("Hints") + market.UnitsToBuy);
                market.ShowHintsAmount();
            }
            else if (market.ItemToBuy == 2)
            {
                PlayerPrefs.SetInt("Hearts", PlayerPrefs.GetInt("Hearts") + market.UnitsToBuy);
                market.ShowHeartsAmount();
            }
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - market.CoinsToPay);
            PlayerPrefs.Save();
            market.ShowCoinsAmount();
            game.CheckHintsCount();
        }
        else
        {
            market.TooltipText.text = UIelement.UIelements[16].text;
        }
    }


}
