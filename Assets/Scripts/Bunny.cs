using UnityEngine;

public class Bunny : MonoBehaviour
{
    public float TimeBeforeStealing = 1f;

    private float timer = 0f;

    private bool isLeaving = false;

    private PieSpot currentSpot = null;

    private void Update()
    {
        if (isLeaving)
            return;

        timer = Mathf.Min(timer + Time.deltaTime, TimeBeforeStealing);
        if (timer.Equals(TimeBeforeStealing))
        {
            if (currentSpot.TryStealPie())
            {
                Leave();
            }
            else
            {
                timer = 0f;
            }
        }
    }

    private void Leave()
    {
        isLeaving = true;
        GameManager.Instance.GetBunnyManager().RemoveBunny(this);
        Destroy(gameObject);
    }

    public void PutInSpot(PieSpot spot)
    {
        currentSpot = spot;
    }

    public PieSpot GetCurrentSpot()
    {
        return currentSpot;
    }
}
