using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void IrAControles()
    {
        SceneManager.LoadScene("Controles");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
