﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMaker : Weather
{
    GameObject player;
    static float maxSpeed = 6.0f;
    float startMaxSpeed;
    private void Start()
    {
        startMaxSpeed = maxSpeed;
    }
    public void SpeedUpDecreasingStamina()
    {       
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Health>().staminaDecreaseSpeed = maxSpeed;
    }

    public void SpeedDownStaminaSpeed()
    {
        maxSpeed -= 1;
    }

    public void RecoverStaminaSpeed()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Health>().staminaDecreaseSpeed = player.GetComponent<Health>().originalStaminaDecreaseSpeed;
        
    }

    public override void OnEnable()
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
    }

    public override void Function()
    {
        SpeedUpDecreasingStamina();

    }
}
