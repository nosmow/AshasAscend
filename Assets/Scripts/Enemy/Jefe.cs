using UnityEngine;
using UnityEngine.UI;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D bossRb;
    [SerializeField] public float vida;
    [SerializeField] private float maximaVida;
    [SerializeField] private Slider barraVidaJefe;
    private bool mirandoDerecha = true;
    public Transform playerTransform;

    // Add a public variable for speed
    [Header("Movimiento")]
    public float velocidadMovimiento = 12f;
    float distanciaJugador;
    
    public void Start()
    {
        vida = maximaVida;
        barraVidaJefe.maxValue = vida;
        barraVidaJefe.value = vida;
        animator = GetComponent<Animator>();
        bossRb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        barraVidaJefe.value = vida;
        distanciaJugador = Vector2.Distance(transform.position, playerTransform.position);
        animator.SetFloat("DistaciaJugador", distanciaJugador);
        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
            Destroy(gameObject, 1f);
        }
    }

    public void MirarJugador()
    {
        // Calculate the difference in position X between the Boss and the player
        float diferenciaX = playerTransform.position.x - transform.position.x;

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

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("Weapon"))
        {
            vida -= Estadisticas.Instance.DañoAEnemigos();
        }
    }

}