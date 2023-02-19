using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Vector3 goal;
    private EnemySpawner enemySpawner;
    private EndGameConditioner endGame;
    private float distanceX;
    private float distanceY;
    [SerializeField] private ParticleSystem bloodSpalter;
    [SerializeField] private float speed = 3, maxSpeed, knockbackSpeed, knockbackTime;
    [SerializeField] private int health = 3;
    [SerializeField] private bool isBoss = false;

    [SerializeField] private GameObject winScream;
    [SerializeField] private GameObject dieScream;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize Enemy Spawner
        enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
        //Initialize End Game Object
        endGame = GameObject.Find("Game Manager").GetComponent<EndGameConditioner>();
        //Initialize their individual goal
        goal = GameObject.Find("Goal").transform.position;
        goal.z += 1;
        goal.z *= Random.Range(-5, 5);
        //SPEED
        maxSpeed *= Random.Range(0.6f, 0.75f);
        speed = maxSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update distance variables always
        distanceX = Mathf.Abs(this.transform.position.x - goal.x);
        distanceY = Mathf.Abs(this.transform.position.y - goal.y);

        //Always look at and move towards goal
        transform.LookAt(goal);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //If too close to goal, call the ReachGoal function
        if(distanceX <= 1.0f && distanceY <= 1.0f)
        {
            ReachGoal();
        }

    }

    //This function will lower the health of this enemy.
    //If the health reaches zero, remove it.
    public void DamageEnemy(int damage)
    {
        health -= damage;
        DamageEffect();

        speed = knockbackSpeed;
        Invoke(nameof(ResetSpeed), knockbackTime);
        
        if(health <= 0)
        {
            BadGuyDies();
        }
        if (isBoss)
        {
            this.gameObject.GetComponent<Boss>().KnockBack(damage);
        }
    }

    private void DamageEffect()
    {
        Instantiate(dieScream);
        bloodSpalter.Play();
    }
    //This function will remove the this enemy and hurt the player
    private void ReachGoal()
    {
        Instantiate(winScream);
        enemySpawner.RemoveBadGuy(this.gameObject);
        endGame.HurtPlayer();
        if (isBoss)
        {
            for(int i = 0; i < 6; i++)
            {
                endGame.HurtPlayer();
            }
        }
    }

    //This function will remove this enemy when called
    private void BadGuyDies()
    {
        enemySpawner.RemoveBadGuy(this.gameObject);
    }

    private void ResetSpeed()
    {
        speed = maxSpeed;
    }
}
