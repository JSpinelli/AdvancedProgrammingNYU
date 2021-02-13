using UnityEngine;

public class SimplePlayer : Player
{

    public SimplePlayer(GameObject pl, float speed, GameObject ball):base(pl, speed, ball){

    }

    public override void Move(){
        playerAvatar.transform.position = Vector3.Lerp(playerAvatar.transform.position, ball.transform.position, speed);
    }
}
