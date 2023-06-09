using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float JumpHeigh = 10f;
    [SerializeField] float ClimbingSpeed = 10f;
    [SerializeField] Vector2 DeathKick = new Vector2(10f, 10f);
    [SerializeField] PhysicsMaterial2D noneMaterial;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    CinemachineStateDrivenCamera stateDrivenCamera;

    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    public Vector2 playerVelocity;
    Vector2 playerScale;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetColider;
    float StartingGravity;
    bool isAlive = true;



    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerScale = transform.localScale;
        animator = GetComponentInChildren<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetColider = GetComponent<BoxCollider2D>();
        StartingGravity = playerRigidbody.gravityScale;
    }
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        SpriteFlip();
        Climb();
        Dead();
    }

    void OnFire()
    {
        if (!isAlive) { return; }
        if (GameSession.Instance.BulletCount > 0)
        {
            Instantiate(bullet, gun.position, transform.rotation);
            GameSession.Instance.BulletCount--;
            GameSession.Instance.BulletsCountText.text = GameSession.Instance.BulletCount.ToString();
        }

    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (value.isPressed && feetColider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRigidbody.velocity += new Vector2(0f, JumpHeigh);
        }
    }

    void SpriteFlip()
    {
        bool HasHorizontalSpeed = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;
        if (HasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerVelocity.x), 1f);
        }
    }

    void Run()
    {
        playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
        bool HasHorizontalSpeed = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", HasHorizontalSpeed);
    }

    void Climb()
    {

        if (!feetColider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerRigidbody.gravityScale = StartingGravity;
            animator.SetBool("isClimbing", false);
            return;
        }

        playerVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * ClimbingSpeed);
        playerRigidbody.velocity = playerVelocity;
        playerRigidbody.gravityScale = 0;
        bool HasVerticalSpeed = Mathf.Abs(playerVelocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", HasVerticalSpeed);
    }

    void Dead()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("BottleEnemy", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            playerRigidbody.velocity = DeathKick;
            bodyCollider.sharedMaterial = noneMaterial;
            feetColider.sharedMaterial = noneMaterial;
            GameSession.Instance.ProcessPlayerDeath();
        }
    }
}
