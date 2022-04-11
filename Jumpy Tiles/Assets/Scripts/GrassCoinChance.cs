using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCoinChance : MonoBehaviour
{
    public GameObject coin;
    public GameObject honey;
    public float chanceToSpawnCoin;
    public float chanceToSpawnHoney;
    void Start()
    {
        SpawnCoin(chanceToSpawnCoin, chanceToSpawnHoney);
    }
    void SpawnCoin(float chanceCoin, float chanceHoney)
    {
        if (Random.value <= chanceCoin)
        {
            Instantiate(coin, new Vector3(transform.position.x, transform.position.y + (float)0.5, transform.position.z), Quaternion.identity);
        }
        else if(Random.value <= chanceCoin + chanceHoney)
        {
            Instantiate(honey, new Vector3(transform.position.x, transform.position.y + (float)1, transform.position.z), Quaternion.identity);
        }
    }
}
