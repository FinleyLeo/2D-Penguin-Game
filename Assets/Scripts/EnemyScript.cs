using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private int maxHealth = 3;
    public int health;

    public float speed = 10;
    public float coolDown = 0;

    private bool isGrounded;
    private bool isDead;

    private GameObject player;
    private SpriteRenderer playerSr;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Sprite");
        playerSr = player.GetComponent<SpriteRenderer>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyMovement()
    {
        if (!isDead)
        {
            if (player.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(1 * speed, rb.velocity.y);
                sr.flipX = false;
            }

            else if (player.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                sr.flipX = true;
            }

            if (Mathf.Abs(rb.velocity.x) > 0)
            {
                anim.SetBool("Run", true);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerSr.color = Color.white;
        anim.SetTrigger("Death");

        isDead = true;
        rb.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}