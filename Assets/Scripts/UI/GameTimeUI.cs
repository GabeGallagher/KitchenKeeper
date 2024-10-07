using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeUI : MonoBehaviour
{
    [SerializeField] private Image timerImg;

    // Update is called once per frame
    void Update()
    {
        timerImg.fillAmount = GameManager.Instance.GetGameTimeNormalized();
    }
}
