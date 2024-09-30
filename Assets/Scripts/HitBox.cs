using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject enemy;

    private SpriteRenderer enemySr;
    private EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = enemy.GetComponent<EnemyScript>();
        enemySr = enemy.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.25f);
        enemySr.color = Color.white;
    }
}
