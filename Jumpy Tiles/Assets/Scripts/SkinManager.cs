using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public int selectedSkin = 0;
    public GameObject Player;
    public GameObject[] AllSkins;
    private void Start()
    {
        selectedSkin = PersitentSkinData.Instance.SelectedSkin;
        ChangeSkin();
    }
    public void ChangeSkin()
    {
        Destroy(Player.transform.GetChild(0).gameObject);
        var newskin =  Instantiate(AllSkins[selectedSkin], Player.transform);
    }
}
