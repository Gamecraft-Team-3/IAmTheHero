using System;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class KnightAttack : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private List<EnemyBehavior> eList;
        [SerializeField] private int damage;
        [SerializeField] private float swingSpeed;
        [SerializeField] private float swingRadius;
        [SerializeField] private float swingTime;

        private void Start()
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
            Destroy(gameObject, swingTime);
            
            if (enemySpawner == null)
                return;
            
            GameObject[] eListTemp = new GameObject[enemySpawner.GetBadGuys().Count];
            enemySpawner.GetBadGuys().CopyTo(eListTemp);

            foreach (var obj in eListTemp)
                eList.Add(obj.GetComponent<EnemyBehavior>());
        }

        private void Update()
        {
            transform.Rotate(new Vector3(0.0f, swingSpeed * Time.deltaTime, 0.0f));
            
            if (enemySpawner == null)
                return;

            foreach (var enemy in eList.ToArray())
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= swingRadius)
                {
                    enemy.DamageEnemy(damage);
                    eList.Remove(enemy);
                }
            }
        }
    }
}