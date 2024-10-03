using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> plateList;

    private void Awake()
    {
        plateList = new List<GameObject>();
    }

    void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plate = plateList[plateList.Count - 1];

        plateList.Remove(plate);

        Destroy(plate);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        GameObject plate = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;

        plate.transform.localPosition = new Vector3(0f, plateOffsetY * plateList.Count, 0f);

        plateList.Add(plate);
    }
}
