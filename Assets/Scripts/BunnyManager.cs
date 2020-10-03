using System.Collections.Generic;
using UnityEngine;

public class BunnyManager : MonoBehaviour
{
    public Bunny[] BunniesPrefabs;
    
    private List<Bunny> currentBunnies = new List<Bunny>();

    public float MinTimeBeforeSpawn = 1f;
    public float MaxTimeBeforeSpawn = 2f;

    public float StepSpawnByLevel = 0.1f;
    public float StepBreakByLevel = 0.1f;
    public float MinTime = 0.001f;

    private float timer = 0f;
    private float spawnTime = 0f;

    private bool isStarted = false;

    private void Awake()
    {
        //debug
        isStarted = true;
    }

    private void Update()
    {
        if (!isStarted)
            return;

        timer = Mathf.Min(timer + Time.deltaTime, spawnTime);
        if (timer.Equals(spawnTime))
        {
            SpawnBunnyInRandomSpot();
            ResetTimer();
        }
    }

    public void StartManager()
    {
        isStarted = true;
    }

    public void StopManager()
    {
        isStarted = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
        spawnTime = UnityEngine.Random.Range(MinTimeBeforeSpawn, MaxTimeBeforeSpawn);
    }

    private void SpawnBunnyInRandomSpot()
    {
        List<PieSpot> spots = GetAvailableSpots();
        SpawnBunny(spots[UnityEngine.Random.Range(0, spots.Count)]);
    }

    private void SpawnBunny(PieSpot pieSpot)
    {
        Bunny bunnyPrefab = BunniesPrefabs[UnityEngine.Random.Range(0, BunniesPrefabs.Length)];

        Bunny newBunny = Instantiate(bunnyPrefab, pieSpot.transform.position, Quaternion.identity);
        newBunny.PutInSpot(pieSpot);
        currentBunnies.Add(newBunny);
    }

    private List<PieSpot> GetAvailableSpots()
    {
        List<PieSpot> availableSpot = new List<PieSpot>(GameManager.Instance.GetRoundManager().GetAllPieSpots());
        foreach (Bunny bun in currentBunnies)
        {
            PieSpot spot = bun.GetCurrentSpot();
            if (spot != null)
            {
                availableSpot.Remove(spot);
            }
        }
        availableSpot.RemoveAll(x => !x.HasPie());
        return availableSpot;
    }

    public void RemoveBunny(Bunny bun)
    {
        currentBunnies.Remove(bun);
    }

    public void RemoveAllBunnies()
    {
        foreach (Bunny bun in currentBunnies)
        {
            Destroy(bun.gameObject);
        }
        currentBunnies.Clear();
    }
}
