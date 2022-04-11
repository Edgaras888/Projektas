using UnityEngine;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public LayerMask Ground;
    private Rigidbody rb;

    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    private int ExplosionDone = 0;
    public BoxCollider playercollider;
    public GameObject[] EnableOnDeath;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        playercollider = player.GetComponent<BoxCollider>();
    }
    public bool isDead()
    {    
        switch (CheckGround())
        {
            case "Death":                
                RemoveConstraints();
                return true;
        }
        return false;
    }
    void RemoveConstraints()
    {
        rb.constraints = RigidbodyConstraints.None;
        PlayerDied();
    }
    string CheckGround()
    {     
        RaycastHit[] hit;
        hit = Physics.CapsuleCastAll(playercollider.bounds.center, new Vector3(playercollider.bounds.center.x, playercollider.bounds.min.y - 0.1f, playercollider.bounds.center.z), 0.3f, Vector3.down, 0.5f , Ground);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    return hit[i].collider.tag;
                }
            }
        return "Death";
    }
    public void DeathExplosion()
    {      

        if (ExplosionDone == 0)
        {

            PlayerDied();
            
           
            //calculate pivot distance
            cubesPivotDistance = cubeSize * cubesInRow / 2;
            //use this value to create pivot vector)
            cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

            //make object disappear
           // RemoveConstraints();
            player.SetActive(false);
            //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
            for (int x = 0; x < cubesInRow; x++)
            {
                for (int y = 0; y < cubesInRow; y++)
                {
                    for (int z = 0; z < cubesInRow; z++)
                    {
                        createPiece(x, y, z);
                    }
                }
            }
           // player.SetActive(false);
            

            //get explosion position
            Vector3 explosionPos = player.transform.GetChild(0).gameObject.transform.position;
            //get colliders in that position and radius
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            //add explosion force to all colliders in that overlap sphere
            foreach (Collider hit in colliders)
            {
                //get rigidbody from collider object
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //add explosion force to this body with given parameters
                    rb.AddExplosionForce(explosionForce, player.transform.GetChild(0).gameObject.transform.position, explosionRadius, explosionUpward);
                }
            }

            
        }

    }

    void createPiece(int x, int y, int z)
    {

        //create piece
        GameObject piece;
      //  piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece = Instantiate(player.transform.GetChild(0).gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        piece.SetActive(true);
        //set piece position and scale
        piece.transform.position = player.transform.GetChild(0).gameObject.transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and set mass
        piece.AddComponent<BoxCollider>();
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }
    void PlayerDied()
    {
        ExplosionDone = 1;
        Invoke("ShowButtons", 3);
    }
    void ShowButtons()
    {
        foreach(GameObject objects in EnableOnDeath)
        {
            objects.SetActive(true);
        }
        FindObjectOfType<HighScore>().ChangeHighscore(FindObjectOfType<Score>().GetScore());
        PersitentSkinData.Instance.SavePlayer();
    }
}
