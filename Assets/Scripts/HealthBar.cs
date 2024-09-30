using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    private SpriteScript health;

    // Start is called before the first frame update
    void Start()
    {
        health = player.GetComponent<SpriteScript>();
        slider.maxValue = health.health;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health.health;
    }
}
