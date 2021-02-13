using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public string owner;
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.CompareTag("Ball")){
            Services.EventManager.Fire(new GoalScored(owner));
            //Old Version
            //Services.gameManager.Score(owner);
        }
    }
}
