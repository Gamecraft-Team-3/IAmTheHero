using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
        enemySpawner.MakeGameDone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
