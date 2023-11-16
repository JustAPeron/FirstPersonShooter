using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
    private int score;
    [HideInInspector] public bool gamePaused;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //Cancel = Esc
        if (Input.GetButtonDown("Cancel"))
            UpdateGamePause();
           
        
    }

    private void UpdateGamePause()
    {
        //change the game pause state
        gamePaused = !gamePaused;

        Time.timeScale = (gamePaused) ? 0.0f : 1.0f;

        Cursor.lockState = (gamePaused) ? CursorLockMode.None : CursorLockMode.Locked;

        //TODO HUD controller
    }
    public void UpdateScore(int points)
    {
        score += points;
        HUDController.instance.UpdateScore(score);
    }
}
