﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    public GameObject[] inGame_UIs;
    void OnEnable()
    {
        for (int i = 0;i< inGame_UIs.Length; i++)
        {
            inGame_UIs[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < inGame_UIs.Length; i++)
        {
            inGame_UIs[i].SetActive(true);
        }
    }
    public void Onclick()
    {
        gameObject.SetActive(false);
        LevelManager.Instance.Resume();
    }
}