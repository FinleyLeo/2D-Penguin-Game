using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    HelperScript helper;

    private float hitPosX = 1f;
    private float hitPosY = -2.5f;
    public float speed = 5f;

    private int enemyDir = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        helper = GetComponent<HelperScript>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (helper.ExtendedRayCollisionCheck(hitPosX, hitPosY) != true)
        {
            enemyDir = -1;
            sr.flipX = true;
            anim.SetBool("Run", true);
        }

        else if (helper.ExtendedRayCollisionCheck(-hitPosX, hitPosY) != true)
        {
            enemyDir = 1;
            sr.flipX = false;
            anim.SetBool("Run", true);
        }

        rb.velocity = new Vector2(enemyDir * speed, 0);
    }

    
}
