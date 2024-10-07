using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    public static OptionsMenuUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton, musicButton, closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText, musicText;

    private bool optionsMenuActive = false;

    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();

            UpdateText();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();

            UpdateText();
        });

        closeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();

            ToggleOptionsMenu();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        gameObject.SetActive(false);

        UpdateText();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void UpdateText()
    {
        soundEffectsText.text = "Sound Effects: " +  Mathf.Round(SoundManager.Instance.Volume * 10f);

        musicText.text = "Music Volume: " + Mathf.Round(MusicManager.Instance.Volume * 10f);
    }

    public void ToggleOptionsMenu()
    {
        optionsMenuActive = !optionsMenuActive;

        if (optionsMenuActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
