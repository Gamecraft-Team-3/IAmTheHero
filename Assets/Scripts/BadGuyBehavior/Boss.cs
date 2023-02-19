using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private int damageDone = 0;
    private float positionX;
    private float positionY;
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
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
        positionZ = this.transform.position.z;
    }

    public void KnockBack(int damage)
    {
        damageDone += damage;
        if(damageDone >= 100)
        {
            this.transform.position = new Vector3(positionX + 5, positionY, positionZ) ;
            damageDone = 0;
        }
    }
}
