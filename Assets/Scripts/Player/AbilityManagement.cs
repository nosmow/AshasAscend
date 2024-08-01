using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManagement : MonoBehaviour
{
    private Slider slider;
    private float sizeSlide = 0;

    private string nameGameObject;
    private bool isWait;

    void Start()
    {
        SizeSlide();

        slider = GetComponentInChildren<Slider>();

        slider.maxValue = sizeSlide;
        slider.value = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isWait)
        {

            if (nameGameObject == "Player")
            {
                Handles_Ability();
                StartCoroutine(WaitForValidate());
            }
        }
    }

    void SizeSlide()
    {
        switch (gameObject.tag)
        {
            case "Vitality":
                sizeSlide = 200;
                break;
            case "agility":
                sizeSlide = 10;
                break;
            case "strenght":
                sizeSlide = 30;
                break;
        }
    }

    void Handles_Ability()
    {
        switch (gameObject.tag)
        {
            case "Vitality":
                Estadisticas.Instance.IncrementVidaMaxima(10);
                slider.value += 10;
                break;
            case "agility":
                Estadisticas.Instance.IncrementJumpForce(1);
                slider.value += 1;
                break;
            case "strenght":
                Estadisticas.Instance.IncrementDañoPlayer(3);
                slider.value += 3;
                break;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        nameGameObject = other.gameObject.name;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nameGameObject = null;
    }

    IEnumerator WaitForValidate()
    {
        isWait = true;
        yield return new WaitForSeconds(0.6f);
        isWait = false;
    }
}
