using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> waveBadGuys = new List<GameObject>();
    [SerializeField] private GameObject badGuyPrefab, bigGuyPrefab, tinyGuyPrefab;
    [SerializeField] private GameObject bossPrefab;

    private int waveCount = 0;
    private int enemyCount = 0;
    private bool isFinalWave = false;
    private bool isDoneWithGame = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveCountdown());
        StartCoroutine(RegularSummon());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Waits a set amount of time before each wave.
    private IEnumerator WaveCountdown()
    {
        yield return new WaitForSeconds(60.0f);
        DetermineEnemyCount();
        StartCoroutine(SummonWave());
    }

    //Summons a wave of enemies.  The amount is determined by another function
    private IEnumerator SummonWave()
    {
        for(int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(0.15f);
            SummonBadGuy();
        }
        if (isFinalWave)
        {
            SummonBoss();
            StopCoroutine(WaveCountdown());
        }
        else
        {
            StartCoroutine(WaveCountdown());
        }
    }

    private IEnumerator RegularSummon()
    {
        yield return new WaitForSeconds(1.35f);
        if (waveCount < 3)
        {
            SummonBadGuy();
            StartCoroutine(RegularSummon());
        }
    }

    //Summons the Bad Guy at a random location
    private void SummonBadGuy()
    {
        int chance = Random.Range(0, 20);

        GameObject selectedInstance;

        if (chance > 2 && chance < 18)
            selectedInstance = badGuyPrefab;
        else if (chance <= 1)
            selectedInstance = tinyGuyPrefab;
        else
            selectedInstance = bigGuyPrefab;
        
        GameObject instance = Instantiate(selectedInstance);
        
        instance.transform.position = GetRandomPosition();
        waveBadGuys.Add(instance);        
    }

    //Summons the big bad guy
    private void SummonBoss()
    {
        GameObject instance = Instantiate(bossPrefab);

        Vector3 pos = GetRandomPosition();

        instance.transform.position = new Vector3(pos.x, pos.y, -2.5f);
        waveBadGuys.Add(instance);
    }

    //Removes a bad guy from the list and destroys it
    public void RemoveBadGuy(GameObject badGuy)
    {
        waveBadGuys.Remove(badGuy);
        Destroy(badGuy);
    }

    //Returns a random Vector3 for a position
    private Vector3 GetRandomPosition()
    {
        float xPos = Random.Range(16, 20);
        float yPos = 0.6f;
        float zPos = Random.Range(-5, 5);

        return new Vector3(xPos, yPos, zPos);

    }

    //Determines how many enemies will spawn depending on the wave
    private void DetermineEnemyCount()
    {
        waveCount++;
        if(waveCount == 1)
        {
            enemyCount = 8;
        }
        else if(waveCount == 2)
        {
            enemyCount = 12;
        }
        else if(waveCount == 3)
        {
            enemyCount = 10;
            isFinalWave = true;
        }
    }

    //Returns true is it is the final wave
    public bool GetWinConditionA()
    {
        return isFinalWave;
    }

    public bool GetWinConditionB()
    {
        if(waveBadGuys.Count != 0)
        {
            return false;
        }

        return true;
    }

    public bool GetWinConditionC()
    {
        return isDoneWithGame;
    }

    public void MakeGameDone()
    {
        isDoneWithGame = true;
    }

    public List<GameObject> GetBadGuys()
    {
        return waveBadGuys;
    }
}
