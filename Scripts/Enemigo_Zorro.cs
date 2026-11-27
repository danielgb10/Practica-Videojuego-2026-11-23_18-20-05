using System;
using UnityEngine;
using UnityEngine.U2D;

public class Enemigo_Zorro : MonoBehaviour
{
    public float tiempoPatrulla;
    public float velocidad;

    public Vector2 tamannoAtaque;
    public float distanciaAtaque;
    public LayerMask capaJugador;

    float temporizador;
    int direccion;
    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D cuerpo;
    bool atacando;
    GameObject aGolpear;
    private bool derecha = true;

    void Start()
    {
        temporizador = tiempoPatrulla;
        direccion = 1;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        cuerpo = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        MoverZorro();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            atacando = true;

            anim.SetTrigger("Atacar");

             collision.gameObject.GetComponent<Gato>().Morir();
        }
    }
    public void FinAtaque()
    {
        atacando = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Limite"))
        {
            derecha = !derecha;
            transform.localScale = new Vector3(-transform.localScale.x,
            transform.localScale.y);
        }
    }

    private bool DetectarJugador()
    {
        RaycastHit2D rayo = Physics2D.BoxCast(
            (Vector2)transform.position, tamannoAtaque, 0, Vector2.right, capaJugador
        );

        if (rayo.collider != null)
        {
            aGolpear = rayo.collider.gameObject;
            return true;
        }

        aGolpear = null;
        return false;
    }

    void OnDrawGizmos()
    {
        if (DetectarJugador())
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(transform.position + Vector3.right * (direccion == 0 ? 1 : direccion),
            tamannoAtaque
        );
        Gizmos.color = Color.white;
    }
}