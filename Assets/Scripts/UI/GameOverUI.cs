using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private int recipesDelivered = 0;

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;

        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;

        Hide();
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        recipesDelivered++;
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            recipesDeliveredText.text = recipesDelivered.ToString();

            Show();
        }
        else
        {
            Hide();
        }
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
