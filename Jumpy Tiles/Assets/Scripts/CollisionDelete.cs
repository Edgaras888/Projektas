using UnityEngine;

public class CollisionDelete : MonoBehaviour
{
    public float timetodelete = 3f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("DeleteObject", timetodelete);
        }
    }
    void DeleteObject()
    {
        Destroy(gameObject);
    }
}
