using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private PlayerController player;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = animator.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsWalking", player.IsWalking);
    }
}
