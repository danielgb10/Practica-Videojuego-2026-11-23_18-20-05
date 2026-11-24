using System;
using UnityEngine;

public class Enemigo_Zorro : MonoBehaviour
{
    /*public Vector3 movimiento;
    SpriteRenderer sprite;*/
    public float tiempoPatrulla;
    public float velocidad = 2f;
    private bool derecha = true;
    public float temporizador;
    int direccion;
    public Vector2 tamannoAtaque;
    public float distanciaAtaque;
    public LayerMask capaJugador;
    Rigidbody2D cuerpo;
    GameObject aGolpear;
    Animator anim;
    private bool atacando = false;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MoverZorro();
        ActualizarTemporizador();

        if (DetectarJugador())
        {

        }
    }

    private void MoverZorro()
    {
        if (atacando) return;

        if (derecha)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        }
    }

    private void ActualizarTemporizador()
    {
        temporizador -= Time.deltaTime;
        if (temporizador < 0)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Limite"))
        {
            derecha = !derecha;
            transform.localScale = new Vector3(-transform.localScale.x,
            transform.localScale.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        atacando = true;
            anim.SetTrigger("Atacar");
        }
        Debug.Log("Enemigo atacando");
    }

    private bool DetectarJugador()
    {
        RaycastHit2D rayo = Physics2D.BoxCast(
            (Vector2)transform.position, tamannoAtaque, 0, Vector2.right * direccion,
            distanciaAtaque, capaJugador
        );

        if (aGolpear)
        {
            GameObject jugador = rayo.rigidbody.gameObject;
            jugador.GetComponent<Gato>().RecibeDanno(1, transform.position);
            return true;
        }
        return false;
    }
    public void FinAtaque()
    {
        atacando = false;
    }

    void OnDrawGizmos()
    {
        if (DetectarJugador())
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(transform.position + Vector3.right * direccion * distanciaAtaque +
            Vector3.down * 0.5f, tamannoAtaque
        );
        Gizmos.color = Color.white;
    }
}