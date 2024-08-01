using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogue;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(turoffDialogue());
    }

    IEnumerator turoffDialogue(){
        yield return new WaitForSeconds(6f);
        this.gameObject.SetActive(false);
    }

}
