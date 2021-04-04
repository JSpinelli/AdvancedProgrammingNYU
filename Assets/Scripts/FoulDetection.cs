using UnityEngine;

public class FoulDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            float impulse = 0F;
            foreach (ContactPoint2D point in other.contacts) {
                impulse += point.normalImpulse;
            }
            Services.EventManager.Fire( new PlayerCollision(impulse / Time.fixedDeltaTime));
        }
    }
}
