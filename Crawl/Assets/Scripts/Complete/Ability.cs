﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject bullet;
    public GameObject fireEx;
    public int damage;
    public int armor;
    public int maxArmor = 100;
    public float avoidance;
    public int hp;
    public int fireExCount;
    
    void Awake()
    {
        hp = GetComponent<Health>().hp;
        damage = bullet.GetComponent<Bullet>().damage;
      //  fireExCount = fireEx.GetComponent<FireExtinguisherItem>().FireExCount; 
    }

    
}