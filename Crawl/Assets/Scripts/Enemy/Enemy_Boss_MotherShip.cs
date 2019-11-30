﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_MotherShip : Enemy
{
    public float stopPos = 371;
    protected GameObject player;
    public GameObject bullet;
    public GameObject[] monsters;
    protected float distance_y;
    protected bool attack = false;
    public float attackDelay = 2.0f;
    public bool onAttack = false;
    [SerializeField] float spinSpeed = 0.2f;
    Transform image;
    // Start is called before the first frame update
    void Start()
    {
        player = LevelManager.Instance.GetPlayer();
        image = transform.GetChild(0);
        print(image.name);
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        image.Rotate(0, 0, spinSpeed);
        image.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;

        if (isPaused)
        {
            speed = 0;
            speed_x = 0;
        }
        else if (isPaused == false)
        {
            speed = originSpeed;
            speed_x = originSpeed_x;
        }
        //print(isPaused);

        distance_y = transform.position.y - player.transform.position.y;
        if (transform.position.y < 59900)
        {
            speed = 0;
            if (attack == false)
            {

                Attack();
            }
            if (transform.position.x < 130 || transform.position.x > 490)
            {
                speed_x = speed_x * -1;
                originSpeed_x = speed_x;
            }
            transform.Translate(speed_x, 0, 0);

        }
        transform.Translate(0, -speed, 0);
    }
    void Attack()
    {
        if (isPaused == false)
        {
            int rand = Random.Range(0, 4);
            print(rand);
            switch (rand)
            {
                case 0:
                    StartCoroutine(SpreadBullet());
                    break;
                case 1:
                    StartCoroutine(RepeatAttack());
                    break;
                case 2:
                    StartCoroutine(SpawnMonster());
                    break;


            }
        }
    }
    IEnumerator SpreadBullet()
    {
        attack = true;
        Enemy_AttackPattern.Instance.MultiShot(gameObject, bullet, Random.Range(3, 7), damage);
        yield return new WaitForSeconds(attackDelay);
        attack = false;
    }
    IEnumerator RepeatAttack()
    {
        attack = true;

        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            float distance_x = transform.position.x - player.transform.position.x;
            float angle = Mathf.Atan2(distance_x, distance_y) * Mathf.Rad2Deg;
            Enemy_AttackPattern.Instance.SingleShot(gameObject, bullet, angle, damage);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(attackDelay);
        attack = false;
    }

    //IEnumerator FireSatelliteBullet()
    //{
    //    attack = true;
    //    float distance_x = transform.position.x - player.transform.position.x;
    //    float distance_y = transform.position.y - player.transform.position.y;
    //    float angle = Mathf.Atan2(distance_x, distance_y) * Mathf.Rad2Deg;
    //    Enemy_AttackPattern.Instance.SingleShot(gameObject, bullet, angle, damage);
    //    yield return new WaitForSeconds(attackDelay);
    //    attack = false;
    //}
    IEnumerator SpawnMonster()
    {
        attack = true;
        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            int rand = Random.Range(0, monsters.Length - 1);
            GameObject monster = Instantiate(monsters[rand]);
        }
        yield return new WaitForSeconds(attackDelay);
        attack = false;
    }

    public override void SetPosition()
    {

        float ypos = player.transform.position.y + 1000;
        transform.position = new Vector3(360, 60900, 0);
    }

    public override void Resume()
    {
        isPaused = false;
    }

    public override void Pause()
    {
        isPaused = true;
    }

    private void OnDisable()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            Destroy(enemys[i]);
        }
        LevelManager.Instance.bossClear = true;
        Item_Explosion.Instance.Explosion(transform.position);
    }
}
