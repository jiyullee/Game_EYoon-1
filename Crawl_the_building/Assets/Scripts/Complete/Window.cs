﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public float HP;
    public float maxHP;
    public Sprite[] brokenWindow;
    public Sprite initialWindow;
    SpriteRenderer sr;
    Image hpImage; 
    public bool itemMade;

    void Start()
    {        
        itemMade = false;        
        hpImage = gameObject.GetComponentInChildren<Image>();
        sr = GetComponent<SpriteRenderer>();
        hpImage.gameObject.SetActive(false);
    }
    public void InitializeWindow()
    {
        sr.sprite = initialWindow;
        hpImage.fillAmount = 1.0f;
        hpImage.gameObject.SetActive(false);
    }
    public void ChangeWindow()
    {
        hpImage.gameObject.SetActive(true);
        hpImage.fillAmount = HP / maxHP;
        if (maxHP * (0.66f) <= HP && HP < maxHP)
        {
            sr.sprite = brokenWindow[0];
        }
        else if (maxHP * (0.33f) <= HP && HP < maxHP * 0.66f)
        {
            sr.sprite = brokenWindow[1];
        }
        else if (0 < HP && HP < maxHP * 0.33f)
        {
            sr.sprite = brokenWindow[2];
        }
        else if (HP <= 0 && itemMade == false)
        {
            sr.sprite = brokenWindow[3];
            ItemManager.Instance.MakeItem(gameObject.transform.position);
            itemMade = true;
        }
    }
   
}
