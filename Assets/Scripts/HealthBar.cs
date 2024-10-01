using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    public Image fill;
    public Gradient gradient;
    private SpriteScript health;

    // Start is called before the first frame update
    void Start()
    {
        health = player.GetComponent<SpriteScript>();
        slider.maxValue = health.health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health.health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
