using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    public Vector3 position;
    [SerializeField] private float innerRadius, outerRadius;

    [SerializeField] private int directDamage, aoeDamage;
    
    private void Start()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();

        if (enemySpawner == null)
            return;
        
        foreach (var bg in enemySpawner.GetBadGuys())
        {
            if (Vector3.Distance(position, bg.transform.position) <= innerRadius)
                bg.GetComponent<EnemyBehavior>().DamageEnemy(directDamage);
            else if (Vector3.Distance(position, bg.transform.position) <= outerRadius)
                bg.GetComponent<EnemyBehavior>().DamageEnemy(aoeDamage);
        }
    }
}
