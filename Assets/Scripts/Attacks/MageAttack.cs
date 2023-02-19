using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    [SerializeField] private float innerRadius, outerRadius;

    [SerializeField] private int directDamage, aoeDamage;

    [SerializeField] private List<EnemyBehavior> eList;

    private void Start()
    {
        Destroy(gameObject, 7.0f);
        
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        
        if (enemySpawner == null)
            return;

        GameObject[] eListTemp = new GameObject[enemySpawner.GetBadGuys().Count];
        enemySpawner.GetBadGuys().CopyTo(eListTemp);

        foreach (var obj in eListTemp) 
            eList.Add(obj.GetComponent<EnemyBehavior>());
        
        foreach (var enemy in eList.ToArray())
            if (Vector3.Distance(transform.position, enemy.transform.position) <= innerRadius)
                enemy.DamageEnemy(directDamage);
        
        Invoke(nameof(DelayedAttack), 1.0f);
    }
    
    private void DelayedAttack()
    {
        foreach (var enemy in eList.ToArray())
        {
            if (enemy == null)
                continue;
            
            if (Vector3.Distance(transform.position, enemy.transform.position) <= outerRadius)
                enemy.DamageEnemy(aoeDamage);
        }
    }
}
