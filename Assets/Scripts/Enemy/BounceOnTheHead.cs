using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnTheHead : MonoBehaviour
{
    public float pushForce = 2f;  // Horizontal thrust force
    public float jumpForce = 5f;  // Small jump strength

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb2d = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb2d != null)
            {
                // Calculates the thrust direction based on the player's scale
                Vector2 pushDirection = collision.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

                // Apply force to the player, including the small jump
                playerRb2d.velocity = new Vector2(pushDirection.x * pushForce, jumpForce);
            }
        }
    }
}
