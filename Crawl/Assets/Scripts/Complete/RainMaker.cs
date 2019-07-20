﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMaker : Weather
{
    GameObject player;
    static float maxSpeed = 6.0f;
    float minSpeed = 1.0f;
    public void DownStaminaSpeed()
    {
        maxSpeed -= 0.1f;
        if (maxSpeed <= minSpeed)
            maxSpeed = minSpeed;
    }
    private void Update()
    {
        print(maxSpeed);
    }
    public void RecoverStaminaSpeed()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Health>().staminaDecreaseSpeed = player.GetComponent<Health>().originalStaminaDecreaseSpeed;
        
    }

    public void OnEnable()
    {
        StartCoroutine(DisableSelf());
    }

    IEnumerator DisableSelf()
    {
        yield return new WaitForSeconds(enableTime);
        gameObject.SetActive(false);
        RecoverStaminaSpeed();
    }

    public override void MakeWeather()
    {
        gameObject.SetActive(true);
        ParticleSystem[] rain = GetComponentsInChildren<ParticleSystem>();
        for (int j = 0; j < rain.Length; j++)
        {
            rain[j].Play();
        }
        Function();
    }

    public override void Function()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Health>().staminaDecreaseSpeed = maxSpeed;
    }
}
