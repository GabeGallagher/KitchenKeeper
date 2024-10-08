using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    public static OptionsMenuUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton, musicButton, closeButton, moveUpButton, moveDownButton, moveLeftButton, moveRightButton, interactButton, interactAltButton, pauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText, musicText, moveUpButtonText, moveDownButtonText, moveLeftButtonText, moveRightButtonText, interactButtonText, interactAltButtonText, pauseButtonText;
    [SerializeField] private Transform pressToRebind;

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

        moveUpButton.onClick.AddListener(() =>
        {
            Rebind(InputManager.Binding.Move_Up);
        });

        moveDownButton.onClick.AddListener(() =>
        {
            Rebind(InputManager.Binding.Move_Down);
        });

        moveLeftButton.onClick.AddListener(() =>
        {
            Rebind(InputManager.Binding.Move_Left);
        });

        moveRightButton.onClick.AddListener(() =>
        {
            Rebind(InputManager.Binding.Move_Right);
        });

        interactButton.onClick.AddListener(() =>
        {
            Rebind(InputManager.Binding.Interact);
        });

        interactAltButton.onClick.AddListener(() =>
        {
            Rebind(InputManager.Binding.Interact_Alternate);
        });

        pauseButton.onClick.AddListener(() =>
        {
            Rebind(InputManager.Binding.Pause);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        HidePressToRebindKey();

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

        moveUpButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Up);

        moveDownButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Down);

        moveRightButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Right);

        moveLeftButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Move_Left);

        interactButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Interact);

        interactAltButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Interact_Alternate);

        pauseButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Pause);
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

    private void ShowPressToRebindKey()
    {
        pressToRebind.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebind.gameObject.SetActive(false);
    }

    private void Rebind(InputManager.Binding binding)
    {
        ShowPressToRebindKey();

        InputManager.Instance.Rebind(binding, () =>
        {
            HidePressToRebindKey();
            UpdateText();
        });
    }
}
