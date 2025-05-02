using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScore : MonoBehaviour
{
    public static UserScore Instance;

    public int userScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        UpdateScore();
        
    }

    public void UpdateScore()
    {
        userScore = MainManager.m_Points;
    }
    
}
