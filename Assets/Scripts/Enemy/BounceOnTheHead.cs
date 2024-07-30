using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnTheHead : MonoBehaviour
{
    public float pushForce = 2f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb2d = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb2d != null)
            {
                // Calcula la dirección del empuje basado en la escala del jugador
                Vector2 pushDirection = collision.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

                // Aplica la fuerza al jugador
                playerRb2d.velocity = new Vector2(pushDirection.x * pushForce, playerRb2d.velocity.y);
            }
        }
    }
}
