using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuScript : MonoBehaviour
{
    public GameObject DeathScreen;
    public Text StuckMeter;
    public void RestartGame()
    {
        DeathScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void DisplayHoneyStuckness(int stucknessCounter)
    {
        stucknessCounter--;
        if(stucknessCounter <= 0)
        {
            StuckMeter.text = "";
        }
        else
        {
            StuckMeter.text = "Stuck " + (stucknessCounter);
        }        
    }
}
