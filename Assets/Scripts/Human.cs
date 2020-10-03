using System.Collections;
using UnityEngine;

public class Human : MonoBehaviour
{
    public float TimeBeforeLeaving = 1f;
    public float WalkSpeed = 5f;

    public float LeaveTime = 2f;

    public Sprite HurtSprite;

    private float timer = 0f;

    private Vector3 startingPosition;
    private Vector3 direction;
    private Transform targetPosition = null;
    private bool hasReachedPosition = false;

    private SpriteRenderer spriteRend;

    private bool isLeaving = false;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();

        startingPosition = transform.position;
    }

    private void Update()
    {
        if (isLeaving)
            return;

        if (!hasReachedPosition)
        {
            if (Vector3.Distance(transform.position, targetPosition.position) > 0.1f)
            {
                transform.position += direction * WalkSpeed * Time.deltaTime;
            }
            else
            {
                hasReachedPosition = true;
            }
        }
        else
        {
            if (!timer.Equals(TimeBeforeLeaving))
            {
                timer = Mathf.Min(timer + Time.deltaTime, TimeBeforeLeaving);
            }
            else
            {
                MoveOutsideScreen();
                if (!spriteRend.isVisible)
                {
                    Leave();
                }
            }
        }
    }

    public void SetTargetPosition(Transform t)
    {
        targetPosition = t;
        direction = targetPosition.position - startingPosition;
    }

    private void MoveOutsideScreen()
    {
        transform.position += direction * WalkSpeed * Time.deltaTime;
    }

    private void OnMouseDown()
    {
        StartCoroutine(Leave());
    }

    private IEnumerator Leave()
    {
        isLeaving = true;
        spriteRend.sprite = HurtSprite;
        yield return new WaitForSeconds(LeaveTime);

        GameManager.Instance.GetHumansManager().RemoveHuman(this);
        Destroy(gameObject);
    }
}
