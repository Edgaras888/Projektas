using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private GameObject teleporterEnd;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = new Vector3(teleporterEnd.transform.position.x, teleporterEnd.transform.position.y + 1, teleporterEnd.transform.position.z);
    }
}
