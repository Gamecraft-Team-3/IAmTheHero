using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWobble : MonoBehaviour
{
    [SerializeField] private float wobbleSpeed;
    [SerializeField] private float wobbleMagnitude;
    [SerializeField] private AudioSource step;
    [SerializeField] private bool repeat;

    private void Start()
    {
        StartCoroutine(StepSound());
    }

    private void Update()
    {
        float sine = (Mathf.Sin(Time.time * wobbleSpeed) * wobbleMagnitude);
        transform.localRotation = Quaternion.Euler(((transform.up + transform.right) * sine) + new Vector3(0.0f, -90.0f, 0.0f));
    }

    private IEnumerator StepSound()
    {
        yield return new WaitForSeconds(1.0f);
        
        do
        {
            step.Play();
            yield return new WaitForSeconds(step.clip.length); 
        } 
        while (repeat);
    }
}
