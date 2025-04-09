using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip obstaclePass;

    [SerializeField] GameObject gameOverCanvas;

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

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        _audioSource.Stop();
        // _audioSource.PlayOneShot(death);
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

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

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        SceneManager.LoadScene(nextSceneIndex);
        PlayerPrefs.SetInt("CurrentScore",Score.instance._score);
        
    }

    public void ReloadGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}