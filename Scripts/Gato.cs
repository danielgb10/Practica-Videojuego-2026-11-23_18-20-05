using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gato : MonoBehaviour
{
    Vector3 movimiento;
    Rigidbody2D cuerpo;
    public float velocidad;
    bool saltando;
    public float fuerzaSalto;
    private float velocidadOriginal;
    private float saltoOriginal;
    public LayerMask capaSuelo;
    public float distCaja;
    public Vector2 tamanoCaja;
    private int monedas; //contador monedas
    public TMP_Text texoMonedas;
    public bool tieneLlave = false;
    SpriteRenderer sprite;
    Animator anim;

    public int vidaMax;
    int vidaRestante;

    public Image corazon1;
    public Image corazon2;
    public Image corazon3;

    private Vector2 knockBack;

    void Start()
    {
        knockBack = Vector2.zero;
        vidaRestante = vidaMax;
        movimiento = new Vector3();
        cuerpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        velocidadOriginal = velocidad;
        saltoOriginal = fuerzaSalto;
        ActualizarCorazones();
    }

    void Update()
    {
        LeerMovimiento();
        LeerSalto();
        ActualizarAnim();
        //EstaEnElSuelo();
    }

    public int Monedas()
    {
        return monedas;
    }

    private void ActualizarAnim()
    {
        if (movimiento.x == 0)
        {
            anim.SetBool("Correr", false);
        }
        else
        {
            anim.SetBool("Correr", true);
        }
        if (!EstaEnElSuelo())
        {
            anim.SetBool("Saltar", true);
        }
        else
        {
            anim.SetBool("Saltar", false);
        }

    }
    void ActualizarCorazones()
    {
        corazon1.enabled = vidaRestante >= 1;
        corazon2.enabled = vidaRestante >= 2;
        corazon3.enabled = vidaRestante >= 3;
    }
    void LeerMovimiento()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movimiento = Vector3.left;
            sprite.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movimiento = Vector3.right;
            sprite.flipX = false;
        }
        else
        {
            movimiento = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        cuerpo.velocity = new Vector2(movimiento.x * velocidad, cuerpo.velocity.y);
        if (saltando)
        {
            cuerpo.AddForce(Vector2.up * fuerzaSalto); 
            saltando = false;
        }
        if (knockBack != Vector2.zero)
        {
            cuerpo.AddForce(knockBack);
            knockBack = Vector2.zero;
        }
    }

    void LeerSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !saltando && EstaEnElSuelo())
        {
            saltando = true;
        }
    }

    bool EstaEnElSuelo()
    {
        RaycastHit2D rc = Physics2D.BoxCast(transform.position, tamanoCaja, 0, Vector2.down, distCaja, capaSuelo);
        if (rc)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RecibeDanno(int danno)
    {
        vidaRestante -= danno;
        Debug.Log("Vida restante: " + vidaRestante);


        ActualizarCorazones();

        if (vidaRestante <= 0) //Destroy(gameObject);
        {
            Morir();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            vidaRestante--;

            ActualizarCorazones();

            if (vidaRestante <= 0)
            {
                Morir();
            }
            else
            {
                anim.SetTrigger("Hit");
            }

        }

        if (collision.CompareTag("Pinchos"))
        {
            vidaRestante--;

            ActualizarCorazones();

            if (vidaRestante <= 0)
            {
                Morir();
            }
            else
            {
                anim.SetTrigger("Hit");
                
            }

        }

        if (collision.CompareTag("Meta"))
        {
            movimiento = Vector3.zero;
            anim.SetTrigger("Victoria");

            Invoke(nameof(MostrarVictoria), 2f);
        }

        if (collision.gameObject.CompareTag("Moneda"))
        {
            Destroy(collision.gameObject);
            monedas++;
            texoMonedas.text = monedas.ToString();
        }

        if (collision.CompareTag("PezRojo"))
        {
            Destroy(collision.gameObject);

            velocidad = velocidadOriginal + 3;

            anim.speed = 2f;

            Invoke(nameof(QuitarVelocidad), 5f);
        }

        if (collision.CompareTag("PezAzul"))
        {
            Destroy(collision.gameObject);

            fuerzaSalto = saltoOriginal + 200;

            anim.speed = 2f;

            Invoke(nameof(QuitarSalto), 5f);
        }
    }
    void MostrarVictoria()
    {
        FindObjectOfType<GanarPerder>().Victoria();
    }

    public void Morir()
    {
        movimiento = Vector3.zero;

        anim.SetTrigger("Derrota");

        Invoke(nameof(MostrarDerrota), 2f);
    }

    void MostrarDerrota()
    {
        FindObjectOfType<GanarPerder>().Derrota();
    }

    void QuitarVelocidad()
    {
        velocidad = velocidadOriginal;

        anim.speed = 1f;
    }
    void QuitarSalto()
    {
        fuerzaSalto = saltoOriginal;

        anim.speed = 1f;
    }
    void OnDrawGizmos()
    {
        if (EstaEnElSuelo())
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireCube(transform.position + Vector3.down * distCaja, tamanoCaja);
        Gizmos.color = Color.white;
    }

}