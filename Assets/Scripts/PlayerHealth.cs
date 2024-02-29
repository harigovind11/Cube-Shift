using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    [SerializeField] int hitpoint = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ProcessHit()
    {
        hitpoint--;
        Death();
    }

    void Death()
    {
        if (hitpoint <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}