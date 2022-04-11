using UnityEngine;
using System.Collections;
public class Chest : MonoBehaviour
{
    private bool IsChestOpen = false;
    private bool EventFinished = true;
    public bool TriggersEvent = false;
    private bool EventHasHappened = false;
    public GameObject GrassOnStepDelete;
    public GameObject Spikes;
    public GameObject ChestEventBlocks;
    public GameObject GeneratedEventBlocks;
    public int ChestTier = 1;
    public GameObject GoldText;
    public GameObject GoldImage;
    public GameObject[] ChestMesh;
    private void Start()
    {
        SelectChestTier();
        PickMeshFromTier();
    }
    public void ActivateChest()
    {
        if(!IsChestOpen && !TriggersEvent && EventFinished && !EventHasHappened)
        {
            IsChestOpen = true;
            AddCoins();        
        }
        else if (TriggersEvent && EventFinished)
        {
            StopCamera();
            EventFinished = false;
            EventHasHappened = true;
            SelectEvent();
        }else if(!IsChestOpen && EventHasHappened && EventFinished)
        {
            IsChestOpen = true;
            CameraScript.UnpauseCamera();
            AddCoins();
        }
    }
    void AddCoins()
    {
        int coins;
        switch (ChestTier)
        {
            case 1:
                coins = Random.Range(100, 101);
                PersitentSkinData.Instance.Coins = PersitentSkinData.Instance.Coins + coins;
                ShowCoins(coins);
            break;
            case 2:
                coins = Random.Range(100, 101);
                PersitentSkinData.Instance.Coins = PersitentSkinData.Instance.Coins + coins;
                ShowCoins(coins);
                break;
            case 3:
                coins = Random.Range(100, 101);
                PersitentSkinData.Instance.Coins = PersitentSkinData.Instance.Coins + coins;
                ShowCoins(coins);
                break;
            default:
                break;
        }

    }
    void StopCamera()
    {
        CameraScript.PauseCamera();
        CameraScript.MoveCameraTo(transform.position.x - 7);
    }
    void ShowCoins(int coins)
    {
        GoldText.GetComponent<TextMesh>().text = coins.ToString();
        GoldText.SetActive(true);
        GoldImage.SetActive(true);
        Invoke("DestroyChest", 2);
    }
    void DestroyChest()
    {
        Destroy(gameObject);
    }
    void SelectChestTier()
    {
        int tiernumber = Random.Range(1, 25);
        if (tiernumber < 17)
        {
            ChestTier = 1;
        }
        else if (tiernumber < 23)
        {
            ChestTier = 2;
        }
        else
        {
            ChestTier = 3;
        }
    }
    void PickMeshFromTier()
    {
        Instantiate(ChestMesh[ChestTier - 1], transform);
    } 
    void SelectEvent()
    {
        int eventnumber = Random.Range((int)1, (int)3);
        switch (eventnumber)
        {
            case 1:
                ChestEventFallingTiles();
                break;
            case 2:
                ChestEventSpawnSpikes();
                break;
            default:
                break;
        }
    }
    void ChestEventFallingTiles()
    {
        ChangeBlock(Random.Range((int)(ChestEventBlocks.transform.childCount * 0.8), ChestEventBlocks.transform.childCount - 1), GrassOnStepDelete);
    }
    void ChestEventSpawnSpikes()
    {
        ChangeBlock(Random.Range((int)(ChestEventBlocks.transform.childCount * 0.5), (int)(ChestEventBlocks.transform.childCount * 0.8)), Spikes);
    }
    void ChangeBlock(int blockAmountToChange, GameObject BlockToChangeInto)
    {
        int blockChangeNumber = Random.Range(0, ChestEventBlocks.transform.childCount-1);

        GameObject blockToChange = ChestEventBlocks.transform.GetChild(blockChangeNumber).gameObject;
        var generatedBlock = Instantiate(BlockToChangeInto, blockToChange.transform.position, blockToChange.transform.rotation, GeneratedEventBlocks.transform);

        Destroy(blockToChange);

        if(blockAmountToChange > 1)
        {
            StartCoroutine(Timer(blockAmountToChange, BlockToChangeInto));
        }
        else
        {
            EventFinished = true;
            TriggersEvent = false;
            StartCoroutine(UnpauseCameraAfterEvent(3f));
        }
    }
    IEnumerator Timer(int blockAmountToChange, GameObject BlockToChangeInto)
    {
        yield return new WaitForSeconds(0.25f);
        ChangeBlock(blockAmountToChange - 1, BlockToChangeInto);
    }
    IEnumerator UnpauseCameraAfterEvent(float time)
    {
        yield return new WaitForSeconds(time);
        CameraScript.UnpauseCamera();
    }
}
