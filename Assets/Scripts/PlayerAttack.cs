using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private SpriteScript script;
    private Animator anim;

    public GameObject enemy;
    private Animator enemyAnim;

    public Transform hitBox;
    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    public int attackDamage = 1;

    private void Start()
    {
        script = GetComponent<SpriteScript>();
        anim = GetComponent<Animator>();

        enemyAnim = enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && script.isGrounded && script.coolDown <= 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyScript>().TakeDamage(attackDamage);
        }

        script.coolDown = 0.25f;
    }

    private void OnDrawGizmosSelected()
    {

        if (hitBox == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(hitBox.position, attackRange);
    }
}