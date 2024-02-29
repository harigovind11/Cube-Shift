using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip obstaclePass;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        Time.timeScale = 1f;
    }

    // public void GameOver()
    // {
    //     gameOverCanvas.SetActive(true);
    //     Time.timeScale = 0f;
    //     _audioSource.Stop();
    //     _audioSource.PlayOneShot(death);
    // }

    public void CrashAudio()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(crash);
    }

    public void PassAudio()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(obstaclePass);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}