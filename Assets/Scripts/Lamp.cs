using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    private bool isBuzzing = false;
    [SerializeField]
    private bool isBlinking = false;

    [SerializeField]
    private AudioSource buzzing = null;
    [SerializeField]
    private List<Light> lights = null;

    private float maxValue = 1f; 

    private void Start()
    {
        if(isBuzzing)
        {
            buzzing.Play();
        }
        maxValue = lights[0].intensity;
    }

    private void Update()
    {
        if(isBlinking)
        {
            var percent = Random.Range(0.5f, 1f);
            foreach(var light in lights)
            {
                light.intensity = maxValue * percent;
            }
        }
    }
}
