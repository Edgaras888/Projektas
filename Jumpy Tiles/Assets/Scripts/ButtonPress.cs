using UnityEngine;
using System.Collections.Generic;

public class ButtonPress : MonoBehaviour
{
    private bool BeenActivated = false;
    [SerializeField] private List<GameObject> MoveDownList;
    private void OnTriggerEnter(Collider other)
    {
        if (!BeenActivated && other.gameObject.tag == "Player")
        {
            BeenActivated = true;
            GetComponent<Animator>().Play("ButtonPress");
            MoveDownObjects();
        }
    }
    private void MoveDownObjects()
    {
        for(int i = 0; i < MoveDownList.Count; i++)
        {
            MoveDownList[i].GetComponent<Animator>().Play("MoveDown");
        }
    }
}
