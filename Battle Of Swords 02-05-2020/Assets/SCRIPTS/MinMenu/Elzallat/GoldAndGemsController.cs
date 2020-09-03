using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAndGemsController : MonoBehaviour
{
    public void BuyGold(int GoldAmount)
    {
        switch (GoldAmount)
        {
            case 7500:
                OnBuyGoldAction(7500, 1000);
                break;
            case 3500:
                OnBuyGoldAction(3500, 500);
                break;
            case 1500:
                OnBuyGoldAction(1500, 150);
                break;
            case 1000:
                OnBuyGoldAction(1000, 80);
                break;
        }
    }

    private void OnBuyGoldAction(int GoldAmount, int GemsPrice)
    {
        if (PlayerVariables.Inistance.getCurrentGems() < GemsPrice)
            StartCoroutine(GeneralController.Inistance.ToastDisplayer("You don't have enough gems"));
        else
        {
            PlayerVariables.Inistance.setCurrentGems(PlayerVariables.Inistance.getCurrentGems() - GemsPrice);
            GeneralController.Inistance.GemsText.text = PlayerVariables.Inistance.getCurrentGems().ToString();
            PlayerVariables.Inistance.setCurrentGold(PlayerVariables.Inistance.getCurrentGold() + GoldAmount);
            GeneralController.Inistance.GoldText.text = PlayerVariables.Inistance.getCurrentGold().ToString();

            StartCoroutine(GeneralController.Inistance.ToastDisplayer(GoldAmount + " Gold Added"));

            //save to playfab
        }
    }

    public void BuyGems(int GemsAmount)
    {
        switch (GemsAmount)
        {
            case 1000:
                IAPManager.Instance.BuyGems_1000();
                break;
            case 500:
                IAPManager.Instance.BuyGems_500();
                break;
            case 150:
                IAPManager.Instance.BuyGems_150();
                break;
            case 80:
                IAPManager.Instance.BuyGems_80();
                break;
        }
    }
}
