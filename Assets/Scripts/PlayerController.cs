using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float sizeRayCast = 1f;
    public float speedPlayer = 5f;
    public float ataquePorSec = 1f;
    public float vidaPlayer = 100f;
    public float rangoDeAtaque = 1.5f;

    [Header("References")]
    public LayerMask enemyLayer;
    public GameObject detectorSuelo;
    public GameObject puntoDeAtaque;
    public LayerMask layer;
    public BarraVidaJefe barraVidaPlayer;
    public Jefe jefe;

    [Header("Special Effects")]
    public GameObject attackEffectPrefab;
    public GameObject jumpEffectPrefab;
    public AudioSource attackSound;

    private float siguienteAtaque;
    private Rigidbody2D playerRb;
    private Animator animator;
    private bool dejarSaltar = false;

    void Start()
    {
        detectorSuelo = GameObject.Find("DetectorSuelo");
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jefe = GameObject.FindGameObjectWithTag("Boss").GetComponent<Jefe>();

        // Initialize player stats
        vidaPlayer = Estadisticas.Instance.vidaMaxima;
        barraVidaPlayer.InicializadorDeBarraDeVida(vidaPlayer);

        // Ensure gravity is set
        Physics2D.gravity *= 2;

        // Initialize audio source if not set
        if (attackSound == null)
        {
            attackSound = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        HandleInput();
        Salto();
    }

    void FixedUpdate()
    {
        Movimiento();
    }

    private void HandleInput()
    {
        if (Time.time >= siguienteAtaque && Input.GetButtonDown("Fire1"))
        {
            PlayerAttack();
            siguienteAtaque = Time.time + 1f / ataquePorSec;
        }
    }

    private void PlayerAttack()
    {
        animator.SetTrigger("Attack");

        // Instantiate the attack effect
        if (attackEffectPrefab != null)
        {
            GameObject effect = Instantiate(attackEffectPrefab, puntoDeAtaque.transform.position, Quaternion.identity);
            Destroy(effect, 2f); // Adjust the duration to match your particle effect's length
        }

        // Play the attack sound
        attackSound?.Play();

        Collider2D[] pegarEnemigos = Physics2D.OverlapCircleAll(puntoDeAtaque.transform.position, rangoDeAtaque, enemyLayer);
        foreach (Collider2D enemy in pegarEnemigos)
        {
            jefe.TomarDaño(Estadisticas.Instance.dañoPlayer);
            Debug.Log("Pegaste a " + enemy.name);
        }
    }

    private void Movimiento()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        animator.SetBool("Run", inputHorizontal != 0);
        animator.SetBool("Idle", inputHorizontal == 0);

        if (inputHorizontal != 0)
        {
            Vector2 movimiento = new Vector2(inputHorizontal * speedPlayer, playerRb.velocity.y);
            playerRb.velocity = movimiento;

            // Flip character
            transform.localScale = new Vector3(Mathf.Sign(inputHorizontal), 1, 1);
        }
    }


    private void Salto()
    {
        Debug.DrawLine(detectorSuelo.transform.position, detectorSuelo.transform.position + Vector3.down * sizeRayCast, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(detectorSuelo.transform.position, Vector2.down, sizeRayCast, layer);
        if (hit.collider != null)
        {
            // Debug para mostrar el objeto que está tocando el rayo
            Debug.Log("El rayo está tocando: " + hit.collider.name);

            dejarSaltar = true;
            if (Input.GetKeyDown(KeyCode.Space) && dejarSaltar)
            {
                dejarSaltar = false;
                animator.SetBool("Idle", false);
                playerRb.AddForce(Vector2.up * Estadisticas.Instance.jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger("Jump");

                // Instanciar el efecto de salto
                if (jumpEffectPrefab != null)
                {
                    GameObject effect = Instantiate(jumpEffectPrefab, transform.position - new Vector3(0, 1.3f, 0), Quaternion.identity);
                    Destroy(effect, 2f); // Ajusta la duración para que coincida con la longitud de tu efecto de partículas
                }
            }
        }
    }

    public void TomarDaño(float daño)
    {
        vidaPlayer -= daño;
        barraVidaPlayer.CambiarVidaActual(vidaPlayer);
        if (vidaPlayer <= 0)
        {
            animator.SetTrigger("Die");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoDeAtaque != null)
        {
            Gizmos.DrawWireSphere(puntoDeAtaque.transform.position, rangoDeAtaque);
        }
    }
}