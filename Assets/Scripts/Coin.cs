using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : MonoBehaviour
{
    [SerializeField] int ScoreOfCoins = 20;
    [SerializeField] AudioClip audioClip;
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<GameSession>().AddScore(ScoreOfCoins);
            AudioSource.PlayClipAtPoint(audioClip, transform.position, 2);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
    }
}
