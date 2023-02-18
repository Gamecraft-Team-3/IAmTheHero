using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform followTransform;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        transform.position = followTransform.position + offset;
    }
}
