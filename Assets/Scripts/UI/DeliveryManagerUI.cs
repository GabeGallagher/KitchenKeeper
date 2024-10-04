using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject container, recipeTemplate;

    private void Awake()
    {
        recipeTemplate.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliverManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliverManager_OnRecipeCompleted;

        UpdateVisual();
    }

    private void DeliverManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliverManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container.transform)
        {
            if (child == recipeTemplate.transform) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.WaitingRecipeList)
        {
            GameObject recipe = Instantiate(recipeTemplate, container.transform);

            recipe.SetActive(true);

            recipe.transform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
