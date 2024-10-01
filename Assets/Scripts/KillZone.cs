using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            if (this.gameObject.CompareTag("Player"))
            {
                gameOver.SetActive(true);
                Destroy(gameObject);
            }

            else if (this.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}
