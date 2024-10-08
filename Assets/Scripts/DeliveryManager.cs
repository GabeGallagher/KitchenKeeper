using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned, OnRecipeCompleted, OnRecipeSuccess, OnRecipeFail;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeList;

    private float spawnTimer, spawnTimerMax = 4f;

    private int waitingRecipesMax = 4;

    public List<RecipeSO> WaitingRecipeList { get => waitingRecipeList; }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeList[i];
            // Has same number of ingredients
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.KitchenObjectSOList.Count)
            {
                // Cycling through all ingredients in recipe
                bool plateContentsMatcheRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    // Cycle through all ingredients in plate
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.KitchenObjectSOList)
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // This Recipe ingredient was not found on plate
                        plateContentsMatcheRecipe = false;
                    }
                }
                if (plateContentsMatcheRecipe)
                {
                    // Player delivered correct recipe
                    waitingRecipeList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFail?.Invoke(this, EventArgs.Empty);
        Debug.Log("Player did not deliver correct meal");
    }

    private void Awake()
    {
        Instance = this;

        waitingRecipeList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTimerMax)
        {
            spawnTimer = 0f;

            if (waitingRecipeList.Count <= waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
