using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private float speed = 12;
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
