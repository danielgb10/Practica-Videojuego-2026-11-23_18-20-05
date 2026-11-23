using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirGato : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posDestino = player.position;
        Vector3 posVieja = transform.position;
        Vector3 posNueva = new Vector3(
            Mathf.Lerp(posVieja.x, posDestino.x, 5 * Time.deltaTime),
            //Mathf.Lerp(posVieja.y, posDestino.y, 5 * Time.deltaTime),
            posVieja.z
        );
        transform.position = posNueva;
    }
}
