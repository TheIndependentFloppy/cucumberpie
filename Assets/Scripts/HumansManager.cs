using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumansManager : MonoBehaviour
{
    public Human[] HumansPrefabs;
    public Transform[] HumansSpots;
    public Transform[] SpawnPoints; //At left or Right screen side

    private List<Human> currentHumans = new List<Human>();

    public float MinTimeBeforeSpawn = 1f;
    public float MaxTimeBeforeSpawn = 2f;

    public float StepSpawnByLevel = 0.1f;
    public float MinTime = 0.001f;

    private float timer = 0f;
    private float spawnTime = 0f;

    private bool isStarted = false;

    private void Update()
    {
        if (!isStarted)
            return;

        timer = Mathf.Min(timer + Time.deltaTime, spawnTime);
        if (timer.Equals(spawnTime))
        {
            SpawnHumanInRandomSpot();
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

    public void UpdateFrequency()
    {
        MinTimeBeforeSpawn -= StepSpawnByLevel;
        if (MinTimeBeforeSpawn < MinTime)
        {
            MinTimeBeforeSpawn = MinTime;
        }
        MaxTimeBeforeSpawn -= StepSpawnByLevel;
        if (MaxTimeBeforeSpawn < MinTime)
        {
            MaxTimeBeforeSpawn = MinTime;
        }
    }

    private void SpawnHumanInRandomSpot()
    {
        List<Transform> spots = GetAvailableSpots();
        SpawnHuman(spots[UnityEngine.Random.Range(0, spots.Count)]);
    }

    private void SpawnHuman(Transform t)
    {
        Human humanPrefab = HumansPrefabs[UnityEngine.Random.Range(0, HumansPrefabs.Length)];

        bool isGoingLeft = Random.value > 0.5f;
        Human newHuman = Instantiate(humanPrefab, isGoingLeft ? SpawnPoints[0].position : SpawnPoints[1].position, Quaternion.identity);
        newHuman.transform.localScale = new Vector3(isGoingLeft ? newHuman.transform.localScale.x * -1f : newHuman.transform.localScale.x, newHuman.transform.localScale.y, 1f); 
        newHuman.SetTargetPosition(t);
        currentHumans.Add(newHuman);
    }


    private List<Transform> GetAvailableSpots()
    {
        List<Transform> availableSpot = new List<Transform>(HumansSpots);
        foreach (Human human in currentHumans)
        {
            Transform pos = availableSpot.Find(x => x.position == human.transform.position);
            if (pos != null)
            {
                availableSpot.Remove(pos);
            }
        }
        return availableSpot;
    }

    public void RemoveHuman(Human human)
    {
        currentHumans.Remove(human);
    }

    public void RemoveAllHumans()
    {
        foreach (Human human in currentHumans)
        {
            Destroy(human.gameObject);
        }
        currentHumans.Clear();
    }
}
