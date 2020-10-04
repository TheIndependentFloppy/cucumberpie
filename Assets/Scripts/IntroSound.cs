using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

}
