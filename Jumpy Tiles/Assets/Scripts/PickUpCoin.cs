using System.Collections;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    private float secondsToDestroy = 60f;
    [SerializeField] private int CoinValue = 1;
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PersitentSkinData.Instance.Coins += CoinValue;
            Destroy(gameObject);
        }
    }
}
