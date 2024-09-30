using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public float coolDown;
    public float speed = 10;
    private bool isGrounded;
    private GameObject player;
    private SpriteScript playerScript;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private SpriteRenderer playerSr;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Sprite");
        playerSr = player.GetComponent<SpriteRenderer>();
        playerScript = player.GetComponent<SpriteScript>();

        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            playerSr.color = Color.white;
            Destroy(gameObject);
        }

        coolDown -= Time.deltaTime;
    }

    void EnemyMovement()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (direction.x > 0)
        {
            sr.flipX = false;
        }

        else
        {
            sr.flipX = true;
        }

        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            anim.SetBool("Run", true);
        }
    }

    void Damage()
    {
        playerScript.health--;
        anim.SetTrigger("Hit");
        StartCoroutine(AttackDuration());
        coolDown = 1f;
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.25f);
        playerSr.color = Color.white;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isGrounded && coolDown <= 0)
        {
            anim.SetTrigger("Attack");
            Damage();
        }

        else if (collision.gameObject.CompareTag("Ground"))
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            health--;
            sr.color = Color.red;
            StartCoroutine(AttackDuration());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player != null)
        {
            EnemyMovement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("Run", false);
    }
}