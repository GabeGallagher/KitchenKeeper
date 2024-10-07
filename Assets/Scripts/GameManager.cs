using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChange, OnGamePaused, OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State gameState;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gameTime;
    private bool isGamePaused = false;

    [SerializeField] private float maxGameTime = 5f;

    public float CountdownToStartTimer { get => countdownToStartTimer; }

    public bool IsGamePlaying()
    {
        return gameState == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return gameState == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return gameState == State.GameOver;
    }

    public float GetGameTimeNormalized()
    {
        return 1 - (gameTime / maxGameTime);
    }

    private void Awake()
    {
        Instance = this;

        gameState = State.WaitingToStart;
    }

    private void Start()
    {
        InputManager.Instance.OnPause += InputManager_OnPause;
    }

    private void InputManager_OnPause(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (gameState)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f)
                {
                    gameState = State.CountdownToStart;

                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    gameState = State.GamePlaying;

                    gameTime = maxGameTime;

                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gameTime -= Time.deltaTime;
                if (gameTime <= 0f)
                {
                    gameState = State.GameOver;

                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if(isGamePaused)
        {
            Time.timeScale = 0f;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
