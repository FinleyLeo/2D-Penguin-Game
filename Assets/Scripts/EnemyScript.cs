using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public int attackDamage = 1;

    public float coolDown = 0;
    public float attackRange = 0.5f;
    public float Xoffset;
    public float Yoffset;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.flipX == true)
        {
            Xoffset = Mathf.Abs(Xoffset) * -1;
        }

        else if (sr.flipX == false)
        {
            Xoffset = Mathf.Abs(Xoffset);
        }

        hitBox.position = new Vector2(transform.position.x + Xoffset, transform.position.y + Yoffset);
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
        anim.SetTrigger("Hit");
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