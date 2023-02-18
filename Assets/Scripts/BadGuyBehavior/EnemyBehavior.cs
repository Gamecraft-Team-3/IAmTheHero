using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Vector3 goal;
    private EnemySpawner enemySpawner;
    private float distanceX;
    private float distanceY;
    [SerializeField] private int speed = 3;
    [SerializeField] private int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
        goal = GameObject.Find("Goal").transform.position;
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
    }

    private void ReachGoal()
    {
        Debug.Log("Hamburger");
        enemySpawner.RemoveBadGuy(this.gameObject);
    }
}
