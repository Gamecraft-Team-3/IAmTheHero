using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private int damageDone = 0;
    private float positionZ;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
        enemySpawner.MakeGameDone();
    }

    // Update is called once per frame
    void Update()
    {
        positionZ = this.transform.position.z;
    }

    public void KnockBack(int damage)
    {
        damageDone += damage;
        if(damageDone >= 100)
        {
            this.transform.position = new Vector3(0, 0, positionZ - 5) ;
            damageDone = 0;
        }
    }
}
