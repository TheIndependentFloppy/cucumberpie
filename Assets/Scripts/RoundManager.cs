﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public float RoundTime = 30f;
    public float TimeBetweenRounds = 3f;

    public Text UIText;

    private float timer = 0f;

    private bool isInRound = false;
    private int currentRound = 0;

    public Pie NormalPiePrefab;

    public AudioClip EndRound;
    public AudioClip BeginRound;

    private PieSpot[] pieSpots;
    private AudioSource audioSource;

    private void Awake()
    {
        pieSpots = FindObjectsOfType<PieSpot>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.Instance.GetMoneyManager().HideRepairMenu();
        UIText.gameObject.SetActive(true);
        UIText.text = "Day " + (currentRound + 1);

        StartCoroutine(InitFirstRound());
    }

    private void Update()
    {
        if (isInRound)
        {
            if (!IsAnyPieLeft())
            {
                UIText.gameObject.SetActive(true);
                Score.Instance.Rounds = currentRound;
                Score.Instance.Money = GameManager.Instance.GetMoneyManager().GetCurrentMoney();
                StopRound();
                SceneManager.LoadScene("Score");
                return;
            }

            timer = Math.Min(timer + Time.deltaTime, RoundTime);
            if (timer.Equals(RoundTime))
            {
                StopRound();
                UIText.gameObject.SetActive(true);
            }
        }
    }

    public void StartRound()
    {
        UIText.text = "Day " + (currentRound + 1);

        currentRound++;
        timer = 0f;

        GameManager.Instance.GetBunnyManager().UpdateFrequency();
        GameManager.Instance.GetMoneyManager().HideRepairMenu();

        UIText.gameObject.SetActive(false);
        GameManager.Instance.GetBunnyManager().StartManager();
        GameManager.Instance.GetHumansManager().StartManager();
        isInRound = true;
    }

    public void StopRound()
    {
        audioSource.PlayOneShot(EndRound);
        GameManager.Instance.GetMoneyManager().ShowRepairMenu();

        GameManager.Instance.GetBunnyManager().StopManager();
        GameManager.Instance.GetBunnyManager().RemoveAllBunnies();
        GameManager.Instance.GetHumansManager().StopManager();
        GameManager.Instance.GetHumansManager().RemoveAllHumans();
        isInRound = false;
        timer = 0f;
    }

    private IEnumerator InitFirstRound()
    {
        audioSource.PlayOneShot(BeginRound);
        foreach (PieSpot spot in pieSpots)
        {
            spot.InitPie(NormalPiePrefab);
        }
        yield return new WaitForSeconds(TimeBetweenRounds);
        StartRound();
    }

    public PieSpot[] GetAllPieSpots()
    {
        return pieSpots;
    }

    private bool IsAnyPieLeft()
    {
        for (int i = 0; i < pieSpots.Length; ++i)
        {
            if (pieSpots[i].HasPie())
            {
                return true;
            }
        }
        return false;
    }

    private PieSpot GetRandomPieSpot()
    {
        List<PieSpot> availableSpot = new List<PieSpot>();
        foreach (PieSpot spot in pieSpots)
        {
            if (spot.HasPie())
            {
                availableSpot.Add(spot);
            }
        }
        if (availableSpot.Count == 0)
            return null;
        return availableSpot[UnityEngine.Random.Range(0, availableSpot.Count)];
    }

    public void SellPie()
    {
        PieSpot spot = GetRandomPieSpot();
        if (spot != null)
        {
            spot.RemovePie();
            GameManager.Instance.GetMoneyManager().SellPie();
        }
    }

    private void RefillPies()
    {
        foreach (PieSpot spot in pieSpots)
        {
            if (!spot.HasPie())
            {
                spot.ReplacePie(NormalPiePrefab);
            }
        }
    }
}
