using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estadisticas : MonoBehaviour
{

    public static Estadisticas Instance { get; private set; }

    public float jumpForce = 0, dañoPlayer = 0, vidaMaxima = 0;
    public float ataqueRecibido = 15;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void IncrementJumpForce(int amount)
    {
        if (jumpForce < 10)
        {
            jumpForce += amount;
        }
    }

    public void IncrementDañoPlayer(int amount)
    {
        if (dañoPlayer < 30)
        {
            dañoPlayer += amount;
        }
    }

    public void IncrementVidaMaxima(int amount)
    {
        if (vidaMaxima < 200)
        {
            vidaMaxima += amount;
        }
    }

    public float Daño()
    {
        return ataqueRecibido;
    }
    public float DañoAEnemigos()
    {
        return dañoPlayer;
    }
}
