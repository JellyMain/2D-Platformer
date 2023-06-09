using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletHorizontalSpeed = 10f;
    [SerializeField] float bulletVerticalSpeed = 5f;
    Rigidbody2D bulletRigidbody;
    PlayerMovement playerMovement;
    float currentPlayerSpeed;
    Animator animator;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        currentPlayerSpeed = playerMovement.transform.localScale.x;
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = new Vector2(currentPlayerSpeed * bulletHorizontalSpeed, bulletVerticalSpeed);
    }
    void Update()
    {
        BulletFlip();
    }
    void BulletFlip()
    {
        bulletRigidbody.transform.localScale = new Vector2 (currentPlayerSpeed, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        //animator.SetBool("BulletCrushed", true);
        Destroy(gameObject);
        
        //animator.Play("BulletCrushed");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //animator.SetBool("BulletCrushed", true);
        Destroy(gameObject);
        
        
    }
}
