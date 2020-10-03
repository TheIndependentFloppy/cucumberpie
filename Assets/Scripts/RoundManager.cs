using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float RoundTime = 30f;
    public float TimeBetweenRounds = 3f;

    private float timer = 0f;

    private bool isInRound = false;
    private int currentRound = 0;

    public Pie NormalPiePrefab;

    private PieSpot[] pieSpots;

    private void Awake()
    {
        pieSpots = FindObjectsOfType<PieSpot>();
    }

    private void Start()
    {
        InitFirstRound();
    }

    private void Update()
    {
        if (isInRound)
        {
            if (!IsAnyPieLeft())
            {
                Debug.Log("GameOver");
                StopRound();
                return;
            }

            timer = Math.Min(timer + Time.deltaTime, RoundTime);
            if (timer.Equals(RoundTime))
            {
                StopRound();
            }
        }
        else
        {
            timer = Math.Min(timer + Time.deltaTime, TimeBetweenRounds);
            if (timer.Equals(TimeBetweenRounds))
            {
                StartRound();
            }
        }
    }

    public void StartRound()
    {
        currentRound++;
        timer = 0f;
        GameManager.Instance.GetBunnyManager().StartManager();
        GameManager.Instance.GetHumansManager().StartManager();
        isInRound = true;
    }

    public void StopRound()
    {
        GameManager.Instance.GetBunnyManager().StopManager();
        GameManager.Instance.GetBunnyManager().RemoveAllBunnies();
        GameManager.Instance.GetHumansManager().StopManager();
        GameManager.Instance.GetHumansManager().RemoveAllHumans();
        isInRound = false;
        timer = 0f;
    }

    private void InitFirstRound()
    {
        foreach (PieSpot spot in pieSpots)
        {
            Pie newPie = Instantiate(NormalPiePrefab, spot.transform.position, Quaternion.identity);
            spot.SetCurrentPie(newPie);
        }
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
}
