using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private int maxHealth = 3;
    public int health;
    public int attackDamage = 1;

    public float speed = 10;
    public float coolDown = 0;
    public float attackRange = 0.5f;
    private float offset;

    public bool isGrounded;
    public bool isDead;

    public Transform hitBox;

    public LayerMask playerLayer;


    private GameObject player;
    private SpriteRenderer playerSr;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private EnemyAttack enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();

        player = GameObject.Find("Sprite");
        playerSr = player.GetComponent<SpriteRenderer>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        hitBox.position = new Vector2(transform.position.x + offset, transform.position.y);
    }

    public void EnemyMovement()
    {
        if (!isDead)
        {
            if (player.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(1 * speed, rb.velocity.y);
                sr.flipX = false;
                offset = 2f;
            }

            else if (player.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                sr.flipX = true;
                offset = -2f;
            }

            if (Mathf.Abs(rb.velocity.x) > 0)
            {
                anim.SetBool("Run", true);
            }
        }
    }

    void Hit()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(hitBox.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<SpriteScript>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {

        if (hitBox == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(hitBox.position, attackRange);
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