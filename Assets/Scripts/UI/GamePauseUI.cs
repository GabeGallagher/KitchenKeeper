using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton, mainMenuButton, optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();

            LoadManager.Load(LoadManager.Scene.MainMenu);
        });

        optionsButton.onClick.AddListener(() =>
        {
            Hide();

            OptionsMenuUI.Instance.ToggleOptionsMenu();
        });
    }

    private void Start()
    {
        gameObject.SetActive(false);

        GameManager.Instance.OnGamePaused += GameManager_OnPause;

        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnPause(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
