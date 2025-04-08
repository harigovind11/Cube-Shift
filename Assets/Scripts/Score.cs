using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    public int _score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
         
        if (PlayerPrefs.GetInt("CurrentScore", _score) != 0)
        {
            _score = PlayerPrefs.GetInt("CurrentScore", _score);
        }
    }


    void Start()
    {
        _currentScoreText.text = PlayerPrefs.GetInt("CurrentScore",_score).ToString();

        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        UpdateHighScore();
    }



    void UpdateHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            _highScoreText.text = _score.ToString();
        }
    }

    public void UpdateScore()
    {
        _score++;

        _currentScoreText.text = _score.ToString();
        UpdateHighScore();
    }

}