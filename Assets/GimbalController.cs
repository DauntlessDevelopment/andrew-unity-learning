﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimbalController : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
