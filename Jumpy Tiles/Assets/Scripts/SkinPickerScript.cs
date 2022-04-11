using UnityEngine;
using UnityEngine.UI;

public class SkinPickerScript : MonoBehaviour
{
    public Text SkinNameText;
    public GameObject Camera;
    public float EnlargeScale = 1f;
    public GameObject ScrollRect;
    public GameObject Content;
    public GameObject[] SkinsToInstanciate;
    public GameObject[] Skins;
    public GameObject PlayButton;
    public static int skinselect = 0;
    public Material BlackMat;

    private void Start()
    {
        // -- Testing
        LoadSkinsOnSceneLoad();
        AddInstanciatedSkins();
        // -- Testing

        RecolorNotOwnedSkins();
        RecenterCamera();    
    }
    void Update()
    {
        CheckSkinPosition();
    }
    public int SelectCharacter()
    {
         for(int i = 0; i < Skins.Length; i++)
         {
            if(Skins[i].transform.position.x > -1 && Skins[i].transform.position.x < 1)
            {
                return i;
            }
         }
        return 0;
    }
    public void CheckSkinPosition()
    {
        skinselect = SelectCharacter();
        CharacterNameText(skinselect);
        SpinCharacter(skinselect);
        EnlargeCharacter(skinselect);
        ReduceCharacter(skinselect);
        ShowButton();
    }
    public void SpinCharacter(int skinselect)
    {
        Skins[skinselect].transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
    public void EnlargeCharacter(int skinselect)
    {
        Skins[skinselect].transform.localScale = new Vector3(EnlargeScale, EnlargeScale, EnlargeScale);
    }
    public void ReduceCharacter(int skinselect)
    {
        for (int i = 0; i < Skins.Length; i++)
        {
            if(i != skinselect)
            {
                Skins[i].transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
    public void ShowButton()
    {       
        if(PersitentSkinData.Instance.OwnedSkins[skinselect])
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
        }
    }
    public void RecolorNotOwnedSkins()
    {
        for(int i = 0; i < Skins.Length; i++)
        {
            if(!PersitentSkinData.Instance.OwnedSkins[i])
            {
                var rend = Skins[i].GetComponent<MeshRenderer>();
                rend.material = BlackMat;
            }
        }
    }
    public void CharacterNameText(int skinselect)
    {
        if(PersitentSkinData.Instance.OwnedSkins[skinselect])
        {
            SkinNameText.text = Skins[skinselect].name;
        }
        else
        {
            SkinNameText.text = "???";
        }      
    }
    public void RecenterCamera()
    {
        Content.transform.position = new Vector3(-0.5f + -2 * PersitentSkinData.Instance.SelectedSkin, Content.transform.position.y, Content.transform.position.z);
    }
    public void LoadSkinsOnSceneLoad()
    {
        RectTransform rectTransform =  ScrollRect.GetComponent<RectTransform>();
        for(int i = 0; i < SkinsToInstanciate.Length; i++)
        {
            Instantiate(SkinsToInstanciate[i], Content.transform.GetChild(0).transform);
            Content.transform.GetChild(0).transform.GetChild(i).transform.position = new Vector3(i * 2, 0, 0);
               
           
            Content.transform.GetChild(0).transform.GetChild(i).transform.rotation = Quaternion.Euler(20, 0, -15);
            Content.transform.GetChild(0).transform.GetChild(i).transform.Rotate(0, 45, 0);
        }
        // Instantiate(AllSkins[selectedSkin], Player.transform);
    }
    public void AddInstanciatedSkins()
    {
        Skins = new GameObject[SkinsToInstanciate.Length];
        for (int i = 0; i < SkinsToInstanciate.Length; i++)
        {         
            Skins[i] = Content.transform.GetChild(0).transform.GetChild(i).transform.gameObject;
            Content.transform.GetChild(0).transform.GetChild(i).transform.gameObject.name = SkinsToInstanciate[i].gameObject.name;
        }
    }
    public void SkinBuy()
    {
        int skinid = Random.Range(1, Skins.Length);
        Debug.Log("SkinBought " + skinid);
        if (PersitentSkinData.Instance.OwnedSkins[skinid] == true)
        {
            Debug.Log("Skin is owned " + skinid);
        }
        else
        {
            var skintochangeto = SkinsToInstanciate[skinid].GetComponent<MeshRenderer>();

            var rend = Skins[skinid].GetComponent<MeshRenderer>();
            rend.material = skintochangeto.sharedMaterial;
            PersitentSkinData.Instance.OwnedSkins[skinid] = true;
        }
        PersitentSkinData.Instance.SavePlayer();
    }
}
