using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> badGuys = new List<GameObject>();
    [SerializeField] private GameObject badGuyPrefab;

    private int waveCount = 0;
    private int enemyCount = 0;
    private bool isFinalWave = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Waits a set amount of time before each wave.
    private IEnumerator WaveCountdown()
    {
        DetermineEnemyCount();
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(SummonWave());
    }

    //Summons a wave of enemies.  The amount is determined by another function
    private IEnumerator SummonWave()
    {

        for(int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
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

    //Summons the Bad Guy at a random location
    private void SummonBadGuy()
    {
        GameObject instance = Instantiate(badGuyPrefab);
        instance.transform.position = GetRandomPosition();
        badGuys.Add(instance);        
    }

    //Summons the big bad guy
    private void SummonBoss()
    {
        Debug.Log("Hey Stinky");
    }

    //Removes a bad guy from the list and destroys it
    public void RemoveBadGuy(GameObject badGuy)
    {
        badGuys.Remove(badGuy);
        Destroy(badGuy);
    }

    //Returns a random Vector3 for a position
    private Vector3 GetRandomPosition()
    {
        float xPos = Random.Range(-5, 5);
        float yPos = 1.0f;
        float zPos = Random.Range(-5, 5);

        return new Vector3(xPos, yPos, zPos);

    }

    //Determines how many enemies will spawn depending on the wave
    private void DetermineEnemyCount()
    {
        waveCount++;
        if(waveCount == 1)
        {
            enemyCount = 7;
        }
        else if(waveCount == 2)
        {
            enemyCount = 18;
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
        if(badGuys.Count != 0)
        {
            return false;
        }

        return true;
    }

    public List<GameObject> GetBadGuys()
    {
        return badGuys;
    }
}
