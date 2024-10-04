using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlayerController player;

    private float footstepTimer, footstepTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.IsWalking)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.audioClipRefSO.footsteps, player.transform.position); 
            }
        }
    }
}
