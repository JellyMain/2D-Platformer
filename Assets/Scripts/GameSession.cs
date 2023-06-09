using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int Score = 0;
    [SerializeField] int PlayerLives = 3;
    [SerializeField] TextMeshProUGUI LivesText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] public TextMeshProUGUI BulletsCountText;
    public int BulletCount = 0;

    public static GameSession Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LivesText.text = PlayerLives.ToString();
        ScoreText.text = Score.ToString();
        BulletsCountText.text = BulletCount.ToString();

    }
    public void ProcessPlayerDeath()
    {
        if (PlayerLives <= 0)
        {
            ReloadTheGame();
        }
        else
        {
            TakeALife();
        }
    }

    public void AddScore(int pointsToAdd)
    {
        Score += pointsToAdd;
        ScoreText.text = Score.ToString();
    }

    public void AddBulletCount()
    {
        BulletCount++;
        BulletsCountText.text = BulletCount.ToString();
    }

    IEnumerator LoadAScene()
    {
        yield return new WaitForSecondsRealtime(2);
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }


    void TakeALife()
    {
        PlayerLives--;
        LivesText.text = PlayerLives.ToString();
        StartCoroutine(LoadAScene());
    }

    void ReloadTheGame()
    {
        SceneManager.LoadScene(0);
    }
}
