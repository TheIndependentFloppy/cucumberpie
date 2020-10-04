using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSound : MonoBehaviour
{
    public AudioClip Gnah;
    public AudioClip BackgroundSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGnahSound()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(Gnah);
    }

    public void PlayBackground1()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(BackgroundSound);
    }

    public void PlayBackground2()
    {
        audioSource.pitch = 0.5f;
        audioSource.PlayOneShot(BackgroundSound);
    }

    public void PlayBackground3()
    {
        audioSource.pitch = 0.8f;
        audioSource.PlayOneShot(BackgroundSound);
    }

    public void PlayBackground4()
    {
        audioSource.pitch = 1.2f;
        audioSource.PlayOneShot(BackgroundSound);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

}
