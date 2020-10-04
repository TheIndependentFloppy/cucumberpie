using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroClickBunny : MonoBehaviour, IPointerClickHandler
{
    public GameObject Menu;
    public GameObject IntroText;

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

    public void PlaySound()
    {
        audioSource.Play();
    }
}
