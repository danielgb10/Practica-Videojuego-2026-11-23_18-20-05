using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptArbol : MonoBehaviour
{
    public GameObject llave;
    public Transform puntoEntrada;
    public Transform puntoSalida;
    public Transform centroCamara;
    private bool dentro = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Gato gatito = collision.gameObject.GetComponent<Gato>();

            if (gatito.tieneLlave)
            {
                if(!dentro)
                {
                    dentro = true;
                    collision.gameObject.transform.position = puntoSalida.position;

               Camera.main.transform.position = new Vector3(
                        centroCamara.position.x,
                        centroCamara.position.y,
                        Camera.main.transform.position.z);              
                }
                else
                {
                    dentro = false;
                    collision.gameObject.transform.position = puntoEntrada.position;
                    
                    Camera.main.transform.position = new Vector3(
                        centroCamara.position.x,
                        centroCamara.position.y,
                        Camera.main.transform.position.z);
                }
            }
        }
    }
}
