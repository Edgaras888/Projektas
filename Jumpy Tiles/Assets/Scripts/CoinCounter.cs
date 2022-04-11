using UnityEngine;
using UnityEngine.UI;
public class CoinCounter : MonoBehaviour
{
    public Text CoinCounterText;
    void Update()
    {      
        CoinCounterText.text = PersitentSkinData.Instance.Coins.ToString();
    }
}
