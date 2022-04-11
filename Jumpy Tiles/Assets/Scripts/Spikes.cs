using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
    public float SecondsBeforeAction = 2f;
    public float AdditionalTime = 0f;
    public Animator animator;
    private bool ActionDone = true;
    private bool StartAdditionalTime = false;
    void Start()
    {
        StartCoroutine(waiter());
    }
    void Update()
    {
        if(ActionDone)
        {
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        ActionDone = false;
        // Invoke(fuction, time to wait before doing)
        yield return new WaitForSeconds(SecondsBeforeAction);
        if(StartAdditionalTime)
        {
            yield return new WaitForSeconds(AdditionalTime);
        }

        animator.Play("Action");
        StartAdditionalTime = true;
    }
    public void ActionEnd()
    {
        ActionDone = true;
    }
}
