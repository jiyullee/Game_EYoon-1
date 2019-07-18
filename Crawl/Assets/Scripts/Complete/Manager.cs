﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : Singleton<Manager>
{
    static public int coin;
    public Text coinText;
    GameObject player;
    bool gameOver = true;
    public GameObject gameOverUI;
    public GameObject scoreUI;
    public GameObject GameCompleteUI;
    float delayTime = 3.0f;
    public GameObject heightUI;
    private void Start()
    {
        coin = 0;
        player = GetComponent<LevelManager>().player;
    }
   
    public void GameComplete()
    {
        StopPlayer();
        GameCompleteUI.SetActive(true);
        StartCoroutine(LoadEndingScene());
    }
    public void Gameover()
    {
        StopPlayer();
        gameOverUI.SetActive(true);
        StartCoroutine(Delay(delayTime));
        gameOver = false;
    }

    public void StopPlayer()
    {
        player.GetComponent<PlayerMove>().forwardSpeed = 0;
    }

    IEnumerator Delay(float delayTime)
    {        
        yield return new WaitForSeconds(delayTime);
        scoreUI.SetActive(true);
        Time.timeScale = 0;

    }
    IEnumerator LoadEndingScene()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("PrefapForge");
    }
    public void IncreaseCoin(int n)
    {
        coin += n;
        coinText.text = coin.ToString();
    }
}
