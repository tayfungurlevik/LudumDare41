using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    [SerializeField]
    private int health = 5;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = health.ToString();
        // Enemy.HandleScore += Enemy_HandleScore;
        PlayerController.OnDamage += PlayerController_OnDamage;
    }

    private void PlayerController_OnDamage()
    {
        health--;
        textMeshPro.text = health.ToString();
    }
    private void OnDestroy()
    {
        PlayerController.OnDamage -= PlayerController_OnDamage;
    }

}
