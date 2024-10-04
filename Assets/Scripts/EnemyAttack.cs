using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyScript enemyScript;
    private Animator anim;

    private GameObject player;
    private SpriteScript playerScript;

    public float coolDown = 2;

    private Collider2D col;

    private bool playerColl;

    private void Start()
    {
        player = GameObject.Find("Sprite");
        playerScript = player.GetComponent<SpriteScript>();

        enemyScript = GetComponentInParent<EnemyScript>();
        anim = GetComponentInParent<Animator>();

        col = gameObject.GetComponent<Collider2D>();

        coolDown = 2;
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;

        if (coolDown < 0 && playerColl && !enemyScript.isDead)
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        coolDown = 2f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.IsTouching(col))
        {
            playerColl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerColl = false;
        }
    }
}
