using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptLlave : MonoBehaviour
{
    private int llave;
    public TMP_Text textoLlave;
    public GameObject texto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            texto.SetActive(true);
            Destroy(texto, 2f);
            Destroy(gameObject);
        }
    }   
}
