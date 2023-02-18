using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> badGuys = new List<GameObject>();
    [SerializeField] private GameObject badGuyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SummonBadGuy();
        SummonBadGuy();
        SummonBadGuy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Summons the Bad Guy at a random location
    private void SummonBadGuy()
    {
        GameObject instance = Instantiate(badGuyPrefab);
        instance.transform.position = GetRandomPosition();
        badGuys.Add(instance);        
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
        float xPos = Random.Range(0, 5);
        float yPos = 0.0f;
        float zPos = Random.Range(0, 5);

        return new Vector3(xPos, yPos, zPos);

    }
}
