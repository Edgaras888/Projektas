using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Startup : MonoBehaviour
{
    public void Awake()
    {
        PersitentSkinData.Instance.LoadPlayer();
    }
}