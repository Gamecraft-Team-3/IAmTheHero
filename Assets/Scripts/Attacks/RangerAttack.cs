using System;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class RangerAttack : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float arrowSpeed;
        [SerializeField] private List<EnemyBehavior> eList;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private float radius;

        private void Start()
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
            Destroy(gameObject, 5.0f);
            
            if (enemySpawner == null)
                return;
            
            GameObject[] eListTemp = new GameObject[enemySpawner.GetBadGuys().Count];
            enemySpawner.GetBadGuys().CopyTo(eListTemp);

            foreach (var obj in eListTemp)
                eList.Add(obj.GetComponent<EnemyBehavior>());
        }

        private void Update()
        {
            transform.position += transform.forward * (arrowSpeed * Time.deltaTime);

            if (enemySpawner == null)
                return;
            
            foreach (var enemy in eList.ToArray())
            {
                if (enemy == null)
                    continue;
                
                if (Vector3.Distance(transform.position, enemy.transform.position) <= radius)
                {
                    enemy.DamageEnemy(damage);
                    eList.Remove(enemy);
                }
            }
        }
    }
}