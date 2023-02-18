using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Vector3 goal;
    private EnemySpawner enemySpawner;
    private float distanceX;
    private float distanceY;
    [SerializeField] private float speed = 3;
    [SerializeField] private int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize Enemy Spawner
        enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
        //Initialize their individual goal
        goal = GameObject.Find("Goal").transform.position;
        goal.z += 1;
        goal.z *= Random.Range(-5, 5);
        //SPEED
        speed *= Random.Range(0.9f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        distanceX = Mathf.Abs(this.transform.position.x - goal.x);
        distanceY = Mathf.Abs(this.transform.position.y - goal.y);

        transform.LookAt(goal);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(distanceX <= 1.0f && distanceY <= 1.0f)
        {
            ReachGoal();
        }
        if(health <= 0)
        {
            BadGuyDies();
        }
    }

    private void ReachGoal()
    {
        enemySpawner.RemoveBadGuy(this.gameObject);
        //Cause damage to the "Player"
    }

    private void BadGuyDies()
    {
        enemySpawner.RemoveBadGuy(this.gameObject);
    }
}
