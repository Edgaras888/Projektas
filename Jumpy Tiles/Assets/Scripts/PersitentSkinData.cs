using UnityEngine;

public class PersitentSkinData : MonoBehaviour
{
    public static PersitentSkinData Instance { get; private set; }
    public int SelectedSkin = 0;
    public int Coins = 0;
    public int HighScore = 0;
    public bool[] OwnedSkins = new bool[100];
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Coins = data.Coins;
        HighScore = data.HighScore;
        SelectedSkin = data.SelectedSkin;        
        OwnedSkins = data.OwnedSkins;
        OwnedSkins[0] = true;
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(HighScore, Coins, SelectedSkin, OwnedSkins);
    }
}
