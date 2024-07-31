using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D bossRb;
    public Transform jugador;
    public LayerMask enemyLayer;
    [SerializeField] public float vida;
    [SerializeField] private float maximaVida;
    [SerializeField] private BarraVidaJefe barraVidaJefe;
    [SerializeField] private PlayerController playerController;
    private bool mirandoDerecha = true;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque = 20;


    // Add a public variable for speed
    [Header("Movimiento")]
    public float velocidadMovimiento = 12f;
    

    // Add a public variable for speed
    [Header("Sound")]

    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip hit;

    public void Start()
    {
        animator = GetComponent<Animator>();
        bossRb = GetComponent<Rigidbody2D>();
        vida = maximaVida;
        barraVidaJefe.InicializadorDeBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Initialize audio source if not set
        if (soundFX == null)
        {
            soundFX = GetComponent<AudioSource>();
        }

    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("DistaciaJugador", distanciaJugador);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        barraVidaJefe.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            Destroy(gameObject, 1f);
            animator.SetTrigger("Die");
        }
    }

    private void Muerte()
    {
        Destroy(gameObject, 1f);
    }

    public void MirarJugador()
    {
        // Calculate the difference in position X between the Boss and the player
        float diferenciaX = jugador.position.x - transform.position.x;

        // Check if the Boss needs to turn around
        bool shouldTurn = (diferenciaX > 0 && !mirandoDerecha) || (diferenciaX < 0 && mirandoDerecha);

        if (shouldTurn)
        {
            // Reverse the gaze direction
            mirandoDerecha = !mirandoDerecha;

            // Rotate the Boss 180 degrees on the Y axis
            transform.Rotate(0, 180, 0);
        }
    }


    public void Ataque()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque, enemyLayer);
        foreach (Collider2D enemy in player)
        {
            if (enemy.CompareTag("Player"))
            {
                enemy.GetComponent<PlayerController>().TomarDaño(dañoAtaque);
                Debug.Log("Pegaste a " + enemy.name);
                soundFX.pitch = Random.Range(0.8f, 1.0f);
                soundFX.PlayOneShot(hit);

            }
        }


        // Play the attack sound
        if (soundFX != null)
        {
            soundFX.pitch = Random.Range(0.8f, 1.0f);
            soundFX.PlayOneShot(attack);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }

    // Method to get the player's transform
    public Transform GetPlayerTransform()
    {
        return jugador;
    }
}