using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptLlave : MonoBehaviour
{
    public GameObject arbol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer dibujo = GetComponent<SpriteRenderer>();
        if(arbol != null)
        {
            arbol.SetActive(false);
        }
    }
}
