using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CounterController.ResetStaticData();

        CuttingCounterController.ResetStaticData();

        TrashCounterController.ResetStaticData();
    }
}
