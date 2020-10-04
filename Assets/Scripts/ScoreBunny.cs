using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBunny : MonoBehaviour, IPointerClickHandler
{
    public Text ScoreText;

    public AudioClip Mlem;
    public AudioClip Outch;

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
        PlayOutchSound();
        anim.SetTrigger("IsTouched");
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    }

    public void PlayMlemSound()
    {
        audioSource.PlayOneShot(Mlem);
    }

    public void PlayOutchSound()
    {
        audioSource.PlayOneShot(Outch);
    }
}
