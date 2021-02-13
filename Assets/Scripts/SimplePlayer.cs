using UnityEngine;

public class SimplePlayer : Player
{

    public SimplePlayer(GameObject pl, float speed):base(pl, speed){

    }

    public override void Update(){
        _gameObject.transform.position = Vector3.Lerp(_gameObject.transform.position, ball.transform.position, speed);
    }
}
