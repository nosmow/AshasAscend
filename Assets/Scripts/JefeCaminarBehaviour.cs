using UnityEngine;

public class JefeCaminarBehaviour : StateMachineBehaviour
{
    private Jefe jefe;
    private Rigidbody2D bossRb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jefe = animator.GetComponent<Jefe>();
        bossRb = jefe.bossRb;
        jefe.MirarJugador();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jefe == null || bossRb == null) return;

        // Ensure that Jefe faces the player
        jefe.MirarJugador();

        // Get the player's position
        Vector2 playerPosition = jefe.GetPlayerTransform().position;

        // Calculate direction towards the player on the x-axis
        float directionX = playerPosition.x - animator.transform.position.x;
        Vector2 direction = new Vector2(directionX, 0).normalized;

        // Ensure the Jefe is not moving when the direction is very small
        if (Mathf.Abs(directionX) > 0.1f)
        {
            // Move the Jefe towards the player on the x-axis using the velocidadMovimiento from Jefe script
            Vector2 targetVelocity = new Vector2(direction.x * jefe.velocidadMovimiento, bossRb.velocity.y);

            // Smoothly adjust the velocity to avoid abrupt changes
            bossRb.velocity = Vector2.Lerp(bossRb.velocity, targetVelocity, Time.deltaTime * 10f);
        }
        else
        {
            // Stop movement if the player is very close
            bossRb.velocity = new Vector2(0, bossRb.velocity.y);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset velocity to stop movement when exiting the state
        bossRb.velocity = new Vector2(0, bossRb.velocity.y);
    }
}
