using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool noChao;
    private animatorRenderer separador;
    private corpoRigido2D corpoRigido;
    public float velocidade;
    public float forcaPulo;
    private Animator animator;
    private float tempoAtaque;
    public float inicioAtaque;

    void Start()
    {
        corpoRigido = GetComponent<corpoRigido2D>();
        separador = GetComponent<animatorRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            corpoRigido.velocity = new Vector2(-velocidade, corpoRigido.velocity.y);

            animator.SetBool("isWalking", true);

            animator.flipX = true;
        }
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            corpoRigido.velocity = new Vector2(velocidade, corpoRigido.velocity.y);

            animator.SetBool("isWalking", true);

            animator.flipX = false;
        }
        else
        {
            corpoRigido.velocity = new Vector2(0, corpoRigido.velocity.y);

            animator.SetBool("isWalking", false);
        }

        if(tempoAtaque <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Z)) {
                animator.SetTrigger("isAttacking");
                tempoAtaque = inicioAtaque;
            }
        }
        else
        {
            tempoAtaque -= Time.deltaTime;
            animator.SetTrigger("isAttacking");
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && noChao)
        {
            corpoRigido.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
            noChao = false;
            animator.SetBool("isJumping", true);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 8)
        {
            noChao = true;
            animator.SetBool("isJumping", false);
        }
    }
}
