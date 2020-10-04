using System.Collections;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    public float TimeBeforeChangingSprite = 0.5f;
    public Sprite ActionSprite = null;
    public Sprite DeadSprite = null;
    public Sprite VictorySprite = null;

    public AudioClip Appear;
    public AudioClip BreakGlass;
    public AudioClip Outch;
    public AudioClip Steal;

    private Sprite startingSprite = null;

    private float timer = 0f;

    private bool isLeaving = false;

    private float timeBeforeStealing = 0f;

    private PieSpot currentSpot = null;
    private SpriteRenderer spriteRend = null;
    private AudioSource audioSource = null;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        startingSprite = spriteRend.sprite;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(Appear);
    }

    private void Update()
    {
        if (isLeaving)
            return;

        if (!currentSpot.HasPie())
        {
            StartCoroutine(Leave());
        }

        timer = Mathf.Min(timer + Time.deltaTime, timeBeforeStealing);
        if (timer.Equals(timeBeforeStealing))
        {
            if (currentSpot.TryStealPie())
            {
                spriteRend.sprite = VictorySprite;
                audioSource.PlayOneShot(Steal);
                StartCoroutine(Leave());
            }
            else
            {
                timer = 0f;
                spriteRend.sprite = ActionSprite;
                audioSource.PlayOneShot(BreakGlass);
                StartCoroutine(GoBackToPreviousSprite());
            }
        }
    }

    public void SetTimeStealing(float time)
    {
        timeBeforeStealing = time;
    }

    private IEnumerator Leave()
    {
        isLeaving = true;
        yield return new WaitForSeconds(TimeBeforeChangingSprite);
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

    private IEnumerator GoBackToPreviousSprite()
    {
        yield return new WaitForSeconds(TimeBeforeChangingSprite);
        spriteRend.sprite = startingSprite;
    }

    public void OnMouseDown()
    {
        if (isLeaving)
            return;
        audioSource.PlayOneShot(Outch);
        spriteRend.sprite = DeadSprite;
        StartCoroutine(Leave());
    }
}
