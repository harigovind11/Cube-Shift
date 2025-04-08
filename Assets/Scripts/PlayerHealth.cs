using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    [SerializeField] int hitpoint = 3;

    public int currentHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentHealth = hitpoint;
        UpdateHearts();
    }

    public void ProcessHit()
    {
        currentHealth--;
        currentHealth = Mathf.Clamp(currentHealth, 0, hitpoint);
        UpdateHearts();
        Death();
    }

    void Death()
    {
        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }



    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
