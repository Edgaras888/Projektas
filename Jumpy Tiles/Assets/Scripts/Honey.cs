using UnityEngine;

public class Honey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement.StuckCounterToRemove = Random.Range(2, 5);
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
    public void RemoveStuckness()
    {
        CharacterMovement.IsStuck = false;
    }
}
