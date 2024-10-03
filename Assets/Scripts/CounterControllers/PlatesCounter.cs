using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : CounterController
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private float spawnTimerMax = 4f;
    [SerializeField] private int plateMax = 3;

    private float spawnTimer;
    private int plateCount;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer > spawnTimerMax)
        {
            spawnTimer = 0f;

            if (plateCount < plateMax)
            {
                plateCount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            if (plateCount > 0)
            {
                plateCount--;

                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
