using UnityEngine;

public class KillOnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().DeathExplosion();
        }
    }
}
