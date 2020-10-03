using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    protected GameManager() { }

    private BunnyManager bunnyManagerInstance;
    private RoundManager roundManagerInstance;
    private HumansManager humansManagerInstance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        bunnyManagerInstance = GetComponentInChildren<BunnyManager>();
        roundManagerInstance = GetComponentInChildren<RoundManager>();
        humansManagerInstance = GetComponentInChildren<HumansManager>();
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
}
