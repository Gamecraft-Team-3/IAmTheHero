using System;
using UnityEngine;

namespace BadGuyBehavior
{
    public class Propellor : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            transform.Rotate(Vector3.up * (speed * Time.deltaTime));
        }
    }
}