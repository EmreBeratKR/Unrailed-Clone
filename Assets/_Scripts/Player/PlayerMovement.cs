using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private MovementChecker movementChecker;
    [SerializeField] private PlayerKeyBinding binding;
    [SerializeField] private Dashing dashing;
    [SerializeField] private float speed;
    [SerializeField] private bool clampDiagonalSpeed;

    private bool isUp => PlayerKeyBinding.isPressed(binding.moveUpKeys);
    private bool isDown => PlayerKeyBinding.isPressed(binding.moveDownKeys);
    private bool isRight => PlayerKeyBinding.isPressed(binding.moveRightKeys);
    private bool isLeft => PlayerKeyBinding.isPressed(binding.moveLeftKeys);
    private bool isDash => PlayerKeyBinding.isPressed(binding.dashKeys);
    private bool isDashing { get => dashing.isDashing; set => dashing.isDashing = value; }
    public Vector3 velocity { get => body.velocity; set => body.velocity = value; }
    public Direction? blockedDirection => movementChecker.blockedDirection;


    private void Update()
    {
        HandleDashing();
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isDashing) return;

        velocity = Vector3.zero;
        
        if (isUp)
        {
            if (blockedDirection != Direction.UP)
            {
                velocity = new Vector3(velocity.x, velocity.y, speed);
            }
        }
        else if (isDown)
        {
            if (blockedDirection != Direction.DOWN)
            {
                velocity = new Vector3(velocity.x, velocity.y, -speed);
            }
        }
        if (isRight)
        {
            if (blockedDirection != Direction.RIGHT)
            {
                velocity = new Vector3(speed, velocity.y, velocity.z);
            }
        }
        else if (isLeft)
        {
            if (blockedDirection != Direction.LEFT)
            {
                velocity = new Vector3(-speed, velocity.y, velocity.z);
            }
        }

        if (clampDiagonalSpeed) velocity = velocity.normalized * speed;
    }

    private void HandleDashing()
    {
        if (isDashing)
        {
            if ((Time.time - dashing.lastDashTime) >= dashing.duration)
            {
                isDashing = false;
                return;
            }
        }

        if (isDash)
        {
            if (isDashing) return;

            if ((Time.time - dashing.lastDashTime) < dashing.cooldown) return;

            isDashing = true;
            dashing.lastDashTime = Time.time;
            velocity = this.transform.forward * dashing.speed;
        }
    }

    private void HandleRotation()
    {
        if (isDashing) return;

        if (isUp && isRight)
        {
            this.transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (isUp && isLeft)
        {
            this.transform.rotation = Quaternion.Euler(0, -45, 0);
        }
        else if (isDown && isRight)
        {
            this.transform.rotation = Quaternion.Euler(0, 135, 0);
        }
        else if (isDown && isLeft)
        {
            this.transform.rotation = Quaternion.Euler(0, -135, 0);
        }
        else if (isUp)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (isDown)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (isRight)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (isLeft)
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}

public enum Direction { UP, DOWN, RIGHT, LEFT }

[System.Serializable]
public struct Dashing
{
    [HideInInspector] public float lastDashTime;
    [HideInInspector] public bool isDashing;
    public float cooldown;
    public float speed;
    public float duration;
}
