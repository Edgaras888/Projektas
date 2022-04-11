using UnityEngine;
using System;
public class CharacterMovement : MonoBehaviour
{
	public GameManager gamemanager;

	public LayerMask obstacles;
	public LayerMask Movingplatforms;

	public Vector3 nextPos;
	public Vector3 currentWorldPos;
	public float jumpForce = 100f;
	public float speed = 0.05f;
	public float speedRot = 0.05f;

	public float rotationOffset = 90;

	public Vector3 facingDir;

	public Animator animator;
	private Animator animatorMesh;

	Rigidbody rb;

	public bool AnimationEnded = true;
	private bool PlayAnimation = true;

	private bool ActionDone = true;
	private bool IsPlayerDead = false;
	private bool DidTryToMove = false;
	private bool NeverResetCamera = false;
	public static bool IsStuck = false;
	private bool CanStartNextMove = true;

	public static int StuckCounterToRemove = 0;
	public GameMenuScript GameMenuScript;

	public GameObject[] EnableOnFirstMove;
	public GameObject[] DisableOnFirstMove;


	void Start()
	{
		currentWorldPos = transform.position;
		rb = GetComponent<Rigidbody>();
		facingDir = Vector3.zero;
		rb.useGravity = false;
		GameMenuScript = FindObjectOfType<GameMenuScript>();
	}





	// Update is called once per frame
	void Update()
	{
		//Debug.Log(StuckCounterToRemove);
		if (StuckCounterToRemove <= 0)
		{
			CanStartNextMove = true;
		}
		if (facingDir != Vector3.zero)
		{
			transform.GetChild(0).gameObject.transform.rotation = Quaternion.RotateTowards(transform.GetChild(0).gameObject.transform.rotation, Quaternion.LookRotation(facingDir), speedRot * Time.deltaTime);
		}
		if (!ActionDone && !IsPlayerDead)
		{
			if (PlayAnimation)
			{
				rb.useGravity = false;
				animatorMesh = transform.GetChild(0).gameObject.GetComponent<Animator>();
				animator.Play("Hop");
				animatorMesh.Play("HopMesh");
				PlayAnimation = false;
			}
			if (transform.position != new Vector3((float)Math.Round(currentWorldPos.x + nextPos.x), transform.position.y, (float)Math.Round(currentWorldPos.z + nextPos.z)) && CanStartNextMove)
			{
				transform.position = Vector3.MoveTowards(transform.position, new Vector3((float)Math.Round(currentWorldPos.x + nextPos.x), transform.position.y, (float)Math.Round(currentWorldPos.z + nextPos.z)), speed * Time.deltaTime);

			}
			else
			{
				if (StuckCounterToRemove > 0)
				{
					CanStartNextMove = false;
					GameMenuScript.DisplayHoneyStuckness((int)StuckCounterToRemove);
				}
				ActionDone = true;
				IsPlayerDead = gamemanager.isDead();
				PlayAnimation = true;
			}
		}
		else
		{
			ActionDone = true;
			nextPos = Vector3.zero;
			currentWorldPos = transform.position;
			//RaycastHit hit;
			if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0 && AnimationEnded)
			{
				DidTryToMove = true;
				ActionDone = false;
				AnimationEnded = false;
				nextPos.z = -Input.GetAxisRaw("Horizontal");
				facingDir = new Vector3(nextPos.z * -1, 0, 0);
				CheckFront();
			}
			else if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") == 0 && AnimationEnded)
			{
				DidTryToMove = true;
				ActionDone = false;
				nextPos.x = Input.GetAxisRaw("Vertical");
				facingDir = new Vector3(0, 0, nextPos.x);
				AnimationEnded = false;
				CheckFront();
			}
			if (!NeverResetCamera && DidTryToMove)
			{
				NeverResetCamera = true;
				StartGame();
			}
		}
	}
	public void CheckFront()
	{
		RaycastHit hit;
		Physics.Raycast(transform.position, nextPos, out hit, 1, obstacles);
		if (hit.collider != null)
		{
			animatorMesh.Play("BumpFront");
			ActionDone = true;
			AnimationEnded = true;
			nextPos = Vector3.zero;
			if (hit.transform.gameObject.tag == "Chest" && AnimationEnded)
			{
				hit.transform.gameObject.SendMessage("ActivateChest");
			}
			if (hit.transform.gameObject.tag == "Honey" && AnimationEnded)
			{
				hit.transform.gameObject.SendMessage("RemoveStuckness");
			}
			AnimationEnded = false;
			Invoke("AnimationEnd", 0.4f);
			return;
		}	
	}
	public void AnimationEnd()
	{
		AnimationEnded = true;
		rb.useGravity = true;
		StuckCounterToRemove--;
	}

	public void StartGame()
	{
		//CameraScript.StartGame = true;
		ShowButtons();
		UnshowButtons();
	}
	void ShowButtons()
	{
		foreach (GameObject objects in EnableOnFirstMove)
		{
			objects.SetActive(true);
		}
	}
	void UnshowButtons()
	{
		foreach (GameObject objects in DisableOnFirstMove)
		{
			objects.SetActive(false);
		}
	}
}
