using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{

    private float speed = 8;
    public float coolDown = 0;
    public float jumpForce = 20;
    private float maxSpeed = 30;


    public int coins;
    public int health;

    private bool isGrounded;
    private bool attacked;
    private bool cam1Active, cam2Active;

    private Vector3 offset = new Vector3(0, 10, -10);

    public GameObject camera1, camera2;
    private GameObject enemy;
    public TextMeshProUGUI coinsText;

    private EnemyScript enemyScript;
    private SpriteRenderer enemySr;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemy = GameObject.FindWithTag("Enemy");
        enemyScript = enemy.GetComponent<EnemyScript>();
        enemySr = enemy.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
        CameraFollow();

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {

        animator.SetBool("walk", false);

        if (Input.GetKey("a") == true)
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            sr.flipX = true;
        }

        if (Input.GetKey("d") == true)
        {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y);
            sr.flipX = false;
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

    void Attack()
    {
        coolDown -= Time.deltaTime;

        if (Input.GetKeyDown("f") && isGrounded && coolDown <= 0)
        {
            animator.SetTrigger("Attack");
            attacked = true;
            coolDown = 0.5f;
        }
    }

    void Damage()
    {
        enemyScript.health--;
        enemySr.color = Color.red;
        StartCoroutine(AttackDuration());
        attacked = false;
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.25f);
        enemySr.color = Color.blue;
    }

    void CameraFollow()
    {
        if (transform.position.x > -2 && cam1Active)
        {
            camera1.transform.position = new Vector3(transform.position.x, camera1.transform.position.y, -10);
        }

        else if (transform.position.x > -2 && cam2Active)
        {
            camera2.transform.position = new Vector3(transform.position.x, camera2.transform.position.y, -10);
        }

        else
        {
            camera1.transform.position = new Vector3(-2, 10, -10);
        }

        if (transform.position.y > 20)
        {
            camera1.SetActive(false);
            camera2.SetActive(true);
            cam2Active = true;
            cam1Active = false;
        }

        else if (transform.position.y < 20)
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
            cam1Active = true;
            cam2Active = false;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && attacked == true)
        {
            Damage();
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
}
