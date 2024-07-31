using TMPro;
using UnityEngine;

public class TrainingController : MonoBehaviour
{
    public float tiempo = 60.0f; // Duración del temporizador
    public TMP_Text tiempoText; // Texto UI para mostrar el temporizador

    private bool tiempoTerminado = false;

    public GameObject progressButton;

    private void Update()
    {
        if (!tiempoTerminado)
        {
            tiempo -= Time.deltaTime; // Restar el tiempo transcurrido

            if (tiempo <= 0)
            {
                tiempo = 0;
                tiempoTerminado = true;

                progressButton.SetActive(true);
            }

            tiempoText.text = "Tiempo: " + Mathf.Ceil(tiempo).ToString("0"); // Actualizar el texto del temporizador
        }
    }
}