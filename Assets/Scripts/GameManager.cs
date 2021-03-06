﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    protected GameManager() { }

    private BunnyManager bunnyManagerInstance;
    private RoundManager roundManagerInstance;
    private HumansManager humansManagerInstance;
    private MoneyManager moneyManagerInstance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        bunnyManagerInstance = GetComponentInChildren<BunnyManager>();
        roundManagerInstance = GetComponentInChildren<RoundManager>();
        humansManagerInstance = GetComponentInChildren<HumansManager>();
        moneyManagerInstance = GetComponentInChildren<MoneyManager>();
    }

    public BunnyManager GetBunnyManager()
    {
        return bunnyManagerInstance;
    }

    public RoundManager GetRoundManager()
    {
        return roundManagerInstance;
    }
    
    public HumansManager GetHumansManager()
    {
        return humansManagerInstance;
    }

    public MoneyManager GetMoneyManager()
    {
        return moneyManagerInstance;
    }
}
