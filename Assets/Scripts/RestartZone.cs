﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag=="Ball"){
            Services.gameManager.Restart();
        }
    }
}
