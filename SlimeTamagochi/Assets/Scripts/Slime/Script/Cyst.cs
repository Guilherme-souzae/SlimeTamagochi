using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyst : MonoBehaviour
{
    public Vector3 velocidadeRotacao = new Vector3(0f, 100f, 0f);

    void Update()
    {
        transform.Rotate(velocidadeRotacao * Time.deltaTime);
    }
}
