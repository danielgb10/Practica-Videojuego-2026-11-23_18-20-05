using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GanarPerder : MonoBehaviour
{
    public GameObject panelVictoria;
    public GameObject panelDerrota;
    public TMP_Text textoVictoria;
    public TMP_Text textoDerrota;

    public void Victoria()
    {
        textoVictoria.text = "Puntuación: " +
        FindObjectOfType<Gato>().Monedas();

        panelVictoria.SetActive(true);
    }

    public void Derrota()
    {
        textoDerrota.text = "Puntuación: " +
            FindObjectOfType<Gato>().Monedas();

        panelDerrota.SetActive(true);
        Time.timeScale = 1f;
    }
    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SalirAlMenu()
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
