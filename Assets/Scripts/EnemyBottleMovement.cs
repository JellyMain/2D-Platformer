using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBottleMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidbody2D;
    [SerializeField]float MoveSpeed = 10f;
    void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        HorizontalMovement();
    }

    void HorizontalMovement()
    {
        enemyRigidbody2D.velocity = new Vector2 (MoveSpeed, 0);
    }

    void FlipTheEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody2D.velocity.x)), 1);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        MoveSpeed = -MoveSpeed;
        FlipTheEnemy();
    }
}
