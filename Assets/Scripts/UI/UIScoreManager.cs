using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScoreManager : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
   
    [SerializeField]
    private int score = 0;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = score.ToString();
       // Enemy.HandleScore += Enemy_HandleScore;
        PlayerController.AddScore += PlayerController_AddScore;
    }

    private void PlayerController_AddScore(float obj)
    {
        this.score += (int)obj;
        textMeshPro.text = score.ToString();
    }

    //private void Enemy_HandleScore(int score)
    //{
    //    this.score *= score;
    //    textMeshPro.text = score.ToString();

    //}

    
}
