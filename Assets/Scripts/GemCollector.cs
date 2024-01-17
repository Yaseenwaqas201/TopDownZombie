using UnityEngine;

public class GemCollector : MonoBehaviour
{
    private bool collectedByPlayer;
    private Transform player;
    public float moveSpeed;
    public void AnimateCollectGem(Transform targetObj)
    {
        collectedByPlayer = true;
        player = targetObj;
    }

    private void Update()
    {
        if (collectedByPlayer)
        {
            // Calculate the direction from the gem to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Normalize the direction vector to ensure consistent movement speed
            directionToPlayer.Normalize();

            // Move the enemy towards the player
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);
        }
        
    }
}
