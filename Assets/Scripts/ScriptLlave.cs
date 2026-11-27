using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptLlave : MonoBehaviour
{
    public GameObject textoLLave;

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
  if (collision.CompareTag("Player"))
    {
        collision.GetComponent<Gato>().tieneLlave = true;

        textoLLave.SetActive(true);

        Destroy(gameObject);
        Destroy(textoLLave, 2f);
    }
}

}
