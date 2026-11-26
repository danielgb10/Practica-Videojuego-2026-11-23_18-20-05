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
    public int vidaMax;

    float temporizador;
    int direccion;
    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D cuerpo;
    bool atacando;
    GameObject aGolpear;
    int vida;


    void Start()
    {
        temporizador = tiempoPatrulla;
        direccion = 1;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        cuerpo = GetComponent<Rigidbody2D>();
        vida = vidaMax;
    }

    void Update()
    {
        Girar();
        ActualizarTemporizador();
        // MoverZorro();
        if (DetectarJugador())
        {
            anim.SetBool("Atacar", true);
            atacando = true;
        }
        else
        {
            anim.SetBool("Atacar", false);
            atacando = false;
        }
    }

   // private void MoverZorro()
    //{
   //     if (atacando) return;
   //
   //     if (derecha)
   //     {
   //         transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    //    }
   //     else
   //     {
   //         transform.Translate(Vector2.left * velocidad * Time.deltaTime);
   //     }
  // }

   // private void OnTriggerEnter2D(Collider2D other)
   // {
     //   if (other.CompareTag("Limite"))
     //   {
    //        derecha = !derecha;
    //        transform.localScale = new Vector3(-transform.localScale.x,
    //        transform.localScale.y);
    //    }
   // }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //   if (collision.gameObject.CompareTag("Player"))
    //   {
    //       atacando = true;
    //
    //       anim.SetTrigger("Atacar");
    //
    //       collision.gameObject.GetComponent<Gato>().Morir();
    //   }
//}

    private bool DetectarJugador()
    {
        RaycastHit2D rayo = Physics2D.BoxCast(
            (Vector2)transform.position, tamannoAtaque, 0, Vector2.right , capaJugador
        );

        if (aGolpear)
        {
            GameObject jugador = rayo.rigidbody.gameObject;
            //jugador.GetComponent<Gato>().RecibeDanno(1, transform.position);
            return true;
        }
        //return false;
        else
        {
            aGolpear = null;
        }
        return false;
    }
    //public void FinAtaque()
    //{
    //    atacando = false;
    //}

    private void Girar()
    {
        if (direccion >= 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    private void ActualizarTemporizador()
    {
        if (!atacando)
        {
            temporizador -= Time.deltaTime;
        }
        if (temporizador < 0)
        {
            direccion *= -1;
            temporizador = tiempoPatrulla;
        }
    }

    void FixedUpdate()
    {
        if (atacando)
        {
            cuerpo.velocity = new Vector2(0, cuerpo.velocity.y);
        }
        else
        {
            cuerpo.velocity = new Vector2(direccion * velocidad, cuerpo.velocity.y);
        }
    }

    public void Aranazo()
    {
        if (aGolpear != null) aGolpear.GetComponent<Gato>().RecibeDanno(1, transform.position);
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
    public void RecibeGolpe(int danno)
    {
        vida -= danno;
        Debug.Log("Zorro vida: " + vida);
        if (vida < 0) Destroy(gameObject);
    }
}