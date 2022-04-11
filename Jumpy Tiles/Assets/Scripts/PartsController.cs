using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsController : MonoBehaviour
{
    public Transform[] parts;
    public Transform[] chestparts;
    public Vector3 nextPartPosition;
    public GameObject player;
    public float partDrawDistance;
    public float partDeleteDistance;

    // Start is called before the first frame update
    void Start()
    {
        LoadParts();
    }

    // Update is called once per frame
    void Update()
    {
        RemoveParts();
        LoadParts();
    }
    void LoadParts()
    {
        while ((nextPartPosition - player.transform.position).x < partDrawDistance)
        {
            int chancenumber = Random.Range(1, 100);
            Transform part;
            if (chancenumber < 95)
            {
                part = parts[Random.Range(0, parts.Length)];
            }
            else
            {
                part = chestparts[Random.Range(0, chestparts.Length)];
            }
            
            Transform newPart = Instantiate(part, nextPartPosition - part.Find("Start Point").position, part.rotation, transform);

            nextPartPosition = newPart.Find("End Point").position;
        }
    }
    void RemoveParts()
    {
        if(transform.childCount > 0)
        {
            Transform part = transform.GetChild(0);
            Vector3 diff = player.transform.position - part.position;

            if(diff.x > partDeleteDistance)
            {
                Destroy(part.gameObject);
            }
        }
    }
}
