[System.Serializable]
public class PlayerData
{
    public int SelectedSkin = 0;
    public int Coins = 0;
    public int HighScore = 0;
    public bool[] OwnedSkins = new bool[100];
    public PlayerData(int highscore, int coins)
    {
        this.HighScore = highscore;
        this.Coins = coins;
    }
    public PlayerData(int highscore, int coins, int selectedSkin, bool[] ownedSkins)
    {
        this.HighScore = highscore;
        this.Coins = coins;
        this.SelectedSkin = selectedSkin;
        this.OwnedSkins = ownedSkins;
    }
}

