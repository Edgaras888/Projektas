using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void SelectedCharacter()
    {
        PersitentSkinData.Instance.SelectedSkin = SkinPickerScript.skinselect;
        PersitentSkinData.Instance.SavePlayer();
        ToGameScene();
    }
    public void ToGameScene()
    {
        SceneManager.LoadScene(0);
    }
}
