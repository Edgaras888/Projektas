using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static float speed = 1f;
    public static bool StartGame = false;
    public static float MoveTo;
    public static bool MoveToLocation = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        StartGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartGame)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), speed * Time.deltaTime);
            if (player.transform.position.x < transform.position.x + 1.5)
            {
                FindObjectOfType<GameManager>().DeathExplosion();
                speed = 0;
            }
            else if (player.transform.position.x > transform.position.x + 8)
            {
                speed = 2;
            }
            else if (player.transform.position.x < transform.position.x + 3)
            {
                speed = 0.5f;
            }
            else
            {
                speed = 1;
            }
        }
        if(MoveToLocation)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(MoveTo, transform.position.y, transform.position.z), speed * Time.deltaTime);
            if(transform.position.x + 0.1 > MoveTo && transform.position.x - 0.1 < MoveTo)
            {
                MoveToLocation = false;
            }
        }
    }
    public static void PauseCamera()
    {
        StartGame = false;
    }
    public static void UnpauseCamera()
    {
        StartGame = true;
    }
    public static void MoveCameraTo(float location)
    {
        MoveToLocation = true;
        MoveTo = location;
    }
}
