using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gato : MonoBehaviour
{
    Vector3 movimiento;
    Rigidbody2D cuerpo;
    public float velocidad;
    bool saltando;
    public float fuerzaSalto;
    public LayerMask capaSuelo;
    public float distCaja;
    public Vector2 tamanoCaja;
    private int monedas; //contador monedas
    public TMP_Text texoMonedas;
    public bool tieneLlave = false;
    SpriteRenderer sprite;
    Animator anim;

    public int vida = 3;
    private Vector2 knockBack;

    void Start()
    {
        movimiento = new Vector3();
        cuerpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        LeerMovimiento();
        LeerSalto();
        ActualizarAnim();
        LeerAtaque();
        //EstaEnElSuelo();
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
        cuerpo.velocity = new Vector2(
            movimiento.x * velocidad,
            cuerpo.velocity.y
        );
        if(saltando)
        {
            cuerpo.AddForce(Vector2.up * fuerzaSalto);
            saltando = false;
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

    private void LeerAtaque()
    {
        if (Input.GetKey(KeyCode.F))
        {
            anim.SetTrigger("Atacar");
        }
        else
        {
            anim.ResetTrigger("Atacar");
        }
    }

    public void RecibeDanno(int cuanto, Vector2 posEnemigo)
    {
        vida -= cuanto;
        Debug.Log("Ouch!! Vida restante: " + vida);
        if (vida <= 0) Destroy(gameObject);
        Vector2 direccionAtaque = ((Vector2)transform.position) - posEnemigo;
        direccionAtaque.Normalize();
        knockBack = direccionAtaque * 250 + 100 * Vector2.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Pinchos"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Moneda"))
        {
            Destroy(collision.gameObject);
            monedas++;
            texoMonedas.text = monedas.ToString();
        }
    }

}