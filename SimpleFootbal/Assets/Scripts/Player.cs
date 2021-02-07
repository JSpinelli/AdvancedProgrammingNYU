using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player
{
    protected GameObject playerAvatar;
    protected float speed;
    protected GameObject ball;

    protected Player(GameObject pl, float speed, GameObject ball){
        this.playerAvatar = pl;
        this.speed = speed;
        this.ball = ball;
    }

    public abstract void Move();
}
