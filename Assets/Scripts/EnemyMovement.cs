using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyScript script;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    public GameObject player;
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        script = GetComponent<EnemyScript>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyMove()
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
