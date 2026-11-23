using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transicion : MonoBehaviour
{
    public Transform origen;
    public Transform destino;
    bool volver;
    float temporizador;

    BoxCollider2D collision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(temporizador > 0)
        {
            temporizador -= Time.deltaTime;
        }
        else
        {
            collision.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!volver) {
            volver = true;
            Camera.main.transform.position = destino.position;
            collision.gameObject.transform.position += 0.5f * new Vector3(1, 0, 0);
        }
        else
        {
            volver = false;
            Camera.main.transform.position = origen.position;
            collision.gameObject.transform.position -= 0.5f * new Vector3(1, 0, 0);
        }

        collision.enabled = false;
        temporizador = 2.0f;
    }
}
