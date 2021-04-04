using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public abstract class Player
{
    protected GameObject _gameObject;
    protected float speed;
    protected GameObject ball;

    public SpriteShapeRenderer playerRenderer;

    public Vector3 position;
    public bool playerTeam;

    public List<Ability> playerAbilities;

    protected Player(GameObject pl, float speed, Ability[] abilities = null)
    {
        _gameObject = pl;
        this.speed = speed;
        this.ball = Services.gameManager.ball;
        position = pl.transform.position;
        playerRenderer = _gameObject.GetComponent<SpriteShapeRenderer>();
        playerAbilities = new List<Ability>();
        if (abilities != null)
            foreach (var ability in abilities)
            {
                Ability toAdd = Object.Instantiate(ability);
                toAdd.SetUpAbility(this);
                playerAbilities.Add(toAdd);
            }
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

    public void GetSurroundings()
    {
    }

    public abstract void Behaviours();

    public void Abilities()
    {
        foreach (var ability in playerAbilities)
        {
            if (ability.ShouldTrigger(this))
            {
                ability.TriggerAbility(this);
            }
            ability.UpdateAbility(this);
        }
    }

    public void Update()
    {
        GetSurroundings();
        Behaviours();
        Abilities();
    }
}