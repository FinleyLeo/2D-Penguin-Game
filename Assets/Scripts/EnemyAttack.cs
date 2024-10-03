using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private SpriteScript script;
    private Animator anim;

    private GameObject player;
    private SpriteScript playerScript;

    public Transform hitBox;

    public float attackRange = 0.5f;
    public float coolDown = 0;

    private bool attackReady;

    public LayerMask playerLayer;

    public int attackDamage = 1;

    private void Start()
    {
        script = GetComponent<SpriteScript>();
        anim = GetComponent<Animator>();

        player = GameObject.Find("Sprite");   
        playerScript = player.GetComponent<SpriteScript>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDown = Time.deltaTime;

        if (coolDown <= 0)
        {

        }

    }

    void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(hitBox.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<SpriteScript>().TakeDamage(attackDamage);
        }

        coolDown = 0.25f;
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
