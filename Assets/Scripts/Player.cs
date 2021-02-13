using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    protected GameObject playerAvatar;
    protected float speed;
    protected GameObject ball;

    public Vector3 position;

    protected Player(GameObject pl, float speed, GameObject ball){
        playerAvatar = pl;
        this.speed = speed;
        this.ball = ball;
        position = pl.transform.position;

    }

    public abstract void Move();
}
