using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float sizeRayCast = 0.1f;
    public float speedPlayer = 5f;
    public float ataquePorSec = 1f;
    public float vidaPlayer = 100f;
    public float rangoDeAtaque = 1.5f;
    public Slider sliderVidaPlayer;

    [Header("References")]
    public GameObject detectorSuelo;
    public GameObject puntoDeAtaque;

    [Header("Special Effects")]
    public GameObject attackEffectPrefab;
    public GameObject jumpEffectPrefab;
    public AudioClip attackSound;
    public AudioClip hitSound;
    private float siguienteAtaque;
    private Rigidbody2D playerRb;
    private Animator animator;
    private bool dejarSaltar = false;

    public Material whiteMaterial;
    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;

    private static bool gravityModified = false;

    void Start()
    {
        detectorSuelo = GameObject.Find("DetectorSuelo");
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (sliderVidaPlayer != null)
        {
            sliderVidaPlayer.maxValue = vidaPlayer;
            sliderVidaPlayer.value = vidaPlayer;
        }


        // Ensure gravity is set
        if (!gravityModified)
        {
            Physics2D.gravity *= 2;
            gravityModified = true;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
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
        if (Time.time >= siguienteAtaque && Input.GetKeyDown(KeyCode.I))
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
            //GameObject effect = Instantiate(attackEffectPrefab, puntoDeAtaque.transform.position, Quaternion.identity);
            //Destroy(effect, 2f); // Adjust the duration to match your particle effect's length
        }

        AudioManager.Instance.PlaySound(attackSound);

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

        dejarSaltar = true;
        if (Input.GetKeyDown(KeyCode.Space) && dejarSaltar)
        {
            RaycastHit2D hit = Physics2D.Raycast(detectorSuelo.transform.position, Vector2.down, sizeRayCast, LayerMask.GetMask("Suelo"));
            if (hit.collider != null)
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

    private void OnTriggerEnter2D(Collider2D other)
    {

        try
        {
            string[] weaponTags = { "BossWeapon", "PlayerWeapon", "Kunai" };

            if (weaponTags.Contains(other.gameObject.tag))
            {
                vidaPlayer -= Estadisticas.Instance.Daño();
                if (sliderVidaPlayer != null)
                {
                    sliderVidaPlayer.value = vidaPlayer;
                }
                AudioManager.Instance.PlaySound(hitSound);

                StartCoroutine(FeedbackDamage());

                
            }
        }
        catch
        {

        }

    }

    IEnumerator FeedbackDamage()
    {
        spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = originalMaterial;
    }
}