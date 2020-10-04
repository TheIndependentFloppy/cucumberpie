using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBunny : MonoBehaviour, IPointerClickHandler
{
    public Text ScoreText;

    private Animator anim;
    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (Score.Instance != null)
        {
            ScoreText.text = "You lasted " + Score.Instance.Rounds + " rounds and made " + Score.Instance.Money + "$";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.SetTrigger("IsTouched");
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
