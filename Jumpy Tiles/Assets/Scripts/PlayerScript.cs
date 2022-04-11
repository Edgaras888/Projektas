using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	public LayerMask obstacles;

	public Vector3 nextPos;
	public Vector3 currentWorldPos;
	public float jumpForce = 100f;
	public float speed = 0.05f;

	public float rotationOffset = 90;

	public Vector3 facingDir;

	public Animator animator;

	Rigidbody rb;

	public int AnimationEnded = 1;

	void Start()
	{
		currentWorldPos = transform.position;
		rb = GetComponent<Rigidbody>();
		facingDir = Vector3.right;
		animator = GetComponent<Animator>();
	}





	// Update is called once per frame
	void Update()
	{
		if (transform.position != new Vector3(currentWorldPos.x + nextPos.x, transform.position.y, currentWorldPos.z + nextPos.z))
		{	
			Debug.Log("J");		
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentWorldPos.x + nextPos.x, transform.position.y, currentWorldPos.z + nextPos.z), speed * Time.deltaTime);		
			animator.Play("Hop");		
		}
		else
		{
			nextPos = Vector3.zero;
			currentWorldPos = transform.position;
			RaycastHit hit;
			if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0 && AnimationEnded == 1)
			{
				AnimationEnded = 0;			
				nextPos.z = -Input.GetAxisRaw("Horizontal");
				Physics.Raycast(transform.position, nextPos, out hit, 1, obstacles);
				if (hit.collider != null)
				{
					AnimationEnded = 1;
					nextPos = Vector3.zero;
					return;
				}			
			}
			else if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") == 0 && AnimationEnded == 1)
			{
				nextPos.x = Input.GetAxisRaw("Vertical");
				AnimationEnded = 0;
				Physics.Raycast(transform.position, nextPos, out hit, 1, obstacles);
				if (hit.collider != null)
				{
					AnimationEnded = 1;
					nextPos = Vector3.zero;
					return;
				}
			}
			
		}	
	}
	public void AnimationEnd()
    {
		AnimationEnded = 1;
    }
}
