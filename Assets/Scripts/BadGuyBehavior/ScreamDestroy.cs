using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathTimer());
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
