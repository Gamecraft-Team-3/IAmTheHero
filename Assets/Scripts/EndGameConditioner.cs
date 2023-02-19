using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameConditioner : MonoBehaviour
{
    private int playerHealth = 5;
    private EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemySpawner.GetWinConditionA() && enemySpawner.GetWinConditionB() && enemySpawner.GetWinConditionC())
        {
            WinGame();
        }
    }

    public void HurtPlayer()
    {
        playerHealth--;
        if(playerHealth < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void WinGame()
    {
        SceneManager.LoadScene("Victory");
    }
}
