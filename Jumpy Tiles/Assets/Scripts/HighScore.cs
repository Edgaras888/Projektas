using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text ScoreText;
    public void ChangeHighscore(int highscore)
    {
        if (PersitentSkinData.Instance.HighScore < highscore)
        {
            PersitentSkinData.Instance.HighScore = highscore;                     
        }
        ScoreText.text = PersitentSkinData.Instance.HighScore.ToString("0");
    }
}
