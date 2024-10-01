using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    private EnemyScript enemyScript;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponentInParent<EnemyScript>();
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyScript.EnemyMovement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("Run", false);
    }
}
