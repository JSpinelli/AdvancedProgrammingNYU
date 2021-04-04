using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public abstract class Player
{
    protected GameObject _gameObject;
    protected float speed;
    protected GameObject ball;

    public AudioSource playerAudioSource;
    public SpriteShapeRenderer playerRenderer;
    
    public Vector3 position;
    public bool playerTeam;

    public Ability[] playerAbilities;

    public bool gotHit = false;

    protected Player(GameObject pl, float speed){
        _gameObject = pl;
        this.speed = speed;
        this.ball = Services.gameManager.ball;
        position = pl.transform.position;
        playerRenderer = _gameObject.GetComponent<SpriteShapeRenderer>();
    }

    public void SetTeam(bool team)
    {
        playerTeam = team;
    }    
    public void SetPosition(Vector3 pos)
    {
        position = pos;
    }

    public void Destroy()
    {
        UnityEngine.Object.Destroy(_gameObject);
    }

    public abstract void Update();
}
