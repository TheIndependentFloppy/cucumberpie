﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text MoneyText;
    public Button NextRoundButton;
    public int PiePrice = 12;
    public int PriceRepair = 33;
    public int MalusCustomer = 40;
    public int ReplacePiePrice = 5;

    private int currentMoney = 0;

    public void SellPie()
    {
        currentMoney += PiePrice;
        RefreshMoneyText();
    }

    public void ApplyMalus()
    {
        currentMoney -= MalusCustomer;
        RefreshMoneyText();
    }

    public int GetCurrentMoney()
    {
        return currentMoney;
    }

    private void RefreshMoneyText()
    {
        MoneyText.text = currentMoney + "$";
    }

    public void ShowRepairMenu()
    {
        NextRoundButton.gameObject.SetActive(true);

        PieSpot[] spots = GameManager.Instance.GetRoundManager().GetAllPieSpots();
        foreach (PieSpot spot in spots)
        {
            spot.SetIsReplacePieButtonInteractable(currentMoney >= ReplacePiePrice);
            spot.SetIsRepairButtonInteractable(currentMoney >= PriceRepair);
            spot.ShowRepairButton();
        }
    }

    public void HideRepairMenu()
    {
        NextRoundButton.gameObject.SetActive(false);
        PieSpot[] spots = GameManager.Instance.GetRoundManager().GetAllPieSpots();
        foreach (PieSpot spot in spots)
        {
            spot.HideButtons();
        }
    }

    public bool IsInRepairMenu()
    {
        return NextRoundButton.gameObject.activeSelf;
    }

    public bool TryRepair()
    {
        if (currentMoney >= PriceRepair)
        {
            currentMoney -= PriceRepair;
            RefreshMoneyText();
            if (currentMoney < PriceRepair)
            {
                DisableRepairButtons();
            }
            if (currentMoney < ReplacePiePrice)
            {
                DisableReplacePieButtons();
            }
            return true;
        }
        return false;
    }

    public bool TryReplacePie()
    {
        if (currentMoney >= ReplacePiePrice)
        {
            currentMoney -= ReplacePiePrice;
            RefreshMoneyText();
            if (currentMoney < ReplacePiePrice)
            {
                DisableReplacePieButtons();
            }
            return true;
        }
        return false;
    }

    private void DisableRepairButtons()
    {
        PieSpot[] spots = GameManager.Instance.GetRoundManager().GetAllPieSpots();
        foreach (PieSpot spot in spots)
        {
            spot.SetIsRepairButtonInteractable(false);
        }
    }

    private void DisableReplacePieButtons()
    {
        PieSpot[] spots = GameManager.Instance.GetRoundManager().GetAllPieSpots();
        foreach (PieSpot spot in spots)
        {
            spot.SetIsReplacePieButtonInteractable(false);
        }
    }
}
