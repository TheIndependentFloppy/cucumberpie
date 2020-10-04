using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroClickBunny : MonoBehaviour, IPointerClickHandler
{
    public GameObject Menu;
    public GameObject IntroText;

    public AudioClip Gnah;
    public AudioClip ClickSound;

    private Animator anim;
    private AudioSource audioSource;

    public void OnPointerClick(PointerEventData eventData)
    {
        anim.SetTrigger("IsTouched");
        StartCoroutine(StartTextIntro());
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator StartTextIntro()
    {
        yield return new WaitForSeconds(1.5f);
        Menu.SetActive(false);
        IntroText.SetActive(true);
    }

    public void PlayGnahSound()
    {
        audioSource.PlayOneShot(Gnah);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(ClickSound);
    }
}
