using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private float aliveTime;
    // Start is called before the first frame update
    void Start()
    {
        aliveTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime -= Time.deltaTime;

        if (aliveTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
