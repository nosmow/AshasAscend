using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManagement : MonoBehaviour
{
    private string nameGameObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)){

            if(nameGameObject == "Player"){
                Debug.Log("entré!");
            }
        }
    }

    void Handles_Ability(){

        switch (gameObject.tag){
            
            case "Vitality":  
            Estadisticas.Instance.IncrementVidaMaxima(1);
            break;
            case "agility": 
            Estadisticas.Instance.IncrementJumpForce(1);
            break;
            case "strenght":
            Estadisticas.Instance.IncrementDañoPlayer(1); 
            break;
            
        }

    } 
   


 void OnTriggerEnter2D(Collider2D other)
 {
    nameGameObject = other.gameObject.name;
 }
}
