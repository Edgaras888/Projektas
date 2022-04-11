using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text Highscore;
    public GameObject Player;
    private int BestScore;
    void Update()
    {
        if (BestScore < (int)Player.transform.position.x)
        {
            Highscore.text = Player.transform.position.x.ToString("0");
            BestScore = (int)Player.transform.position.x;
        }
    }
    public int GetScore()
    {
        return BestScore;
    }
}
