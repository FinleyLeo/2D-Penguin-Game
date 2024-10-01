using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteScript : MonoBehaviour
{

    private readonly float speed = 8;
    private readonly float jumpForce = 20;
    private readonly float maxSpeed = 30;

    public int coins;
    public int health;

    public bool isGrounded;

    public GameObject cam;
    private GameObject enemy;

    public TextMeshProUGUI coinsText;
    public GameObject gameOver;

    private EnemyScript enemyScript;
    private SpriteRenderer enemySr;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public Animator animator;
    private HelperScript helper;

    public GameObject hitBox;
    private float offset = 1.45f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemy = GameObject.FindWithTag("Enemy");
        enemyScript = enemy.GetComponent<EnemyScript>();
        enemySr = enemy.GetComponent<SpriteRenderer>();

        helper = gameObject.AddComponent<HelperScript>();

        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CameraFollow();

        if (Input.GetKeyDown(KeyCode.H))
        {
            helper.Hello();
        }
    }

    void Move()
    {

        animator.SetBool("walk", false);

        if (Input.GetKey("a") == true)
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            sr.flipX = true;
            hitBox.transform.position = new Vector2(transform.position.x - offset, transform.position.y + 1.5f);
        }

        if (Input.GetKey("d") == true)
        {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y);
            sr.flipX = false;
            hitBox.transform.position = new Vector2(transform.position.x + offset, transform.position.y + 1.5f);
        }

        if (Mathf.Abs(rb.velocity.x) > 0.1)
        {
            animator.SetBool("walk", true);
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);
        }

        if (rb.velocity.y <= 0 && !isGrounded)
        {
            animator.SetBool("Falling", true);
        }
    }

    void CameraFollow()
    {
        if (transform.position.x > -10)
        {
            cam.transform.position = new Vector3(transform.position.x, transform.position.y + 5, -10);
        }

        else if (transform.position.x < -10)
        {
            cam.transform.position = new Vector3(-10, transform.position.y + 5, -10);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            gameOver.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = true;
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("HitGround", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("HitGround", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
            coinsText.text = coins.ToString();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Rope"))
        {
            isGrounded = true;
        }
    }
}
