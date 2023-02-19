using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWobble : MonoBehaviour
{
    [SerializeField] private float wobbleSpeed;
    [SerializeField] private float wobbleMagnitude;

    private void Update()
    {
        float sine = (Mathf.Sin(Time.time * wobbleSpeed) * wobbleMagnitude);
        transform.localRotation = Quaternion.Euler(((transform.up + transform.right) * sine) + new Vector3(0.0f, -90.0f, 0.0f));
    }
}
