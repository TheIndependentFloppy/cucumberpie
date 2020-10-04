using UnityEngine;

public class Score : MonoBehaviour
{
    public int Rounds;
    public int Money;

    public static Score Instance = null;

    protected Score() { }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
