﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMaker : MonoBehaviour
{
    HazardManager HazardManager;
    GameObject player;
    [SerializeField] int dist = 80;
    int delay = 4;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        HazardManager = GetComponent<HazardManager>();
        Debug.Log(HazardManager);
        StartCoroutine(DetectPlayer());
        

    }
    
    public IEnumerator DetectPlayer()
    {
        for (int i = 0; i < 100; i++)
        {
            if (transform.position.y - player.transform.position.y < 165 && transform.position.y - dist > player.transform.position.y)
            {
                HazardManager.SpawnHazard(gameObject.transform.position);
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
        }
    }

}
