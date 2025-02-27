﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Agent
{
    public Text health_text;
    public Image healthBarFill;

    private Vector3 direction = Vector3.zero;

    public GameObject gimbal;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        gimbal = GameObject.FindGameObjectWithTag("Gimbal");
        movement_speed = 6f;
    }

    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            Move(direction);
            Turn(direction);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        // Convert input to direction - should this be moved to a separate component?
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            direction = Quaternion.Euler(0f, gimbal.transform.rotation.eulerAngles.y, 0f) * new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        }
        else
        {
            direction = Vector3.zero;
        }

        if (Input.GetAxisRaw("Fire1") == 1)
        {
            rightHandAction();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "SpawnTrigger")
        {
            GameManager gM = ServiceLocator.Get<GameManager>();
            // Do not spawn more than one enemy per 2 seconds
            if (gM.lastSpawnTime + 2f < Time.timeSinceLevelLoad)
                GameManager.SpawnEntityInArea(gM.enemy, other.transform.position, 10f);
        }
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        health_text.text = $"Health : {health}";
        healthBarFill.fillAmount = health / maxHealth;
    }
}
