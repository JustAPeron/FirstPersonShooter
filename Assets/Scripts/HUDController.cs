using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;    
    }

    public void UpdateHealthBar(float health)
    {
        healthBarImage.fillAmount = health;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString("00000");
    }

}
