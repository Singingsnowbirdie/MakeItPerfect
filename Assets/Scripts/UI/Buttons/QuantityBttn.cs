using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantityBttn : MonoBehaviour
{
    enum Action
    {
        subtract, add
    }

    [SerializeField] Action currentAction;

    MarketPanel market;
    AudioSource clickSound;


    private void Awake()
    {
        market = FindObjectOfType<MarketPanel>();
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();
    }

    public void Press()
    {
        clickSound.Play();

        if (currentAction == Action.subtract)
        {
            if (market.UnitsToBuy > 1)
            {
                market.UnitsToBuy--;
            }
        }
        else
        {
            if (market.ItemToBuy == 1)
            {
                if (PlayerPrefs.GetInt("Coins") > market.CoinsToPay + 7)
                {
                    market.UnitsToBuy++;
                }
            }
            else if (market.ItemToBuy == 2)
            {
                if (PlayerPrefs.GetInt("Coins") > market.CoinsToPay + 21)
                {
                    market.UnitsToBuy++;
                }
            }
        }
        market.ShowUnitsToBuy();
    }

}
