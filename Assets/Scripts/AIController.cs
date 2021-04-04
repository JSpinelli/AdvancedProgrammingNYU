
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIController
{
    private List<Player> _players;
    private Ability[] abilities;

    #region Lifecycle Management
    
    public void Initialize()
    {
        abilities = Services.gameManager.allAbilities;
        _players = new List<Player>();
        //_players.Add(new ForcePlayer(Services.gameManager.player2,Services.gameManager.movementSpeed));
        _players.Add(new DefensivePlayer(Services.gameManager.player2,Services.gameManager.movementSpeed,abilities[0]));
    }

    public Player GetClosestAI(Ball ball)
    {
        if (_players.Count == 0) return null;

        var closest = _players[0];
        var distance = float.MaxValue;

        foreach (var player in _players)
        {
            var distanceToPlayer = Vector3.Distance(ball.transform.position, player.position);
            if (distanceToPlayer < distance)
            {
                closest = player;
                distance = distanceToPlayer;
            }
        }

        return closest;
    }

    public void Update()
    {
        foreach (var player in _players)
        {
            player.Update();
        }
    }

    public void Destroy()
    {
        foreach (var player in _players)
        {
            player.Destroy();
        }
    }

    #endregion
}