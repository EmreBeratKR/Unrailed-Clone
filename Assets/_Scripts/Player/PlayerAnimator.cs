using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Animator animator;


    private void Update()
    {
        if (movement.velocity.magnitude != 0f)
        {
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }
    }
}
