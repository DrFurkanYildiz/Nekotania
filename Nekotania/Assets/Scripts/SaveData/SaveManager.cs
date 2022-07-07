using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    //private string saveFiles = "SaveGame";
    private BuildManager manager;
    private void Awake()
    {
        Instance = this;
        manager = GetComponent<BuildManager>();
    }

    public void OnSave()
    {
        Rest.SaveObject RestSaveObject = Rest.Instance.GetSaveObject();
        Castle.SaveObject CastleSaveObject = Castle.Instance.GetSaveObject();
        Mating.SaveObject MatingSaveObject = Mating.Instance.GetSaveObject();
        LightHouse.SaveObject LightHouseSaveObject = LightHouse.Instance.GetSaveObject();
        Military.SaveObject MilitarySaveObject = Military.Instance.GetSaveObject();

        BuildManager.SaveObject managerSaveObject = manager.GetSaveObject();
        CycleManager.SaveObject cycleManagerSaveObject = CycleManager.Instance.GetSaveObject();
        OlayIsleyiciScript.SaveObject olayIsleyiciSaveObject = OlayIsleyiciScript.Instance.GetSaveObject();
        GameEventManager.SaveObject gameEventSaveObject = GameEventManager.Instance.GetSaveObject();

        List<Cat.SaveObject> catSaveObjectList = new List<Cat.SaveObject>();
        List<PuzzleHandler.SaveObject> puzzleObjectList = new List<PuzzleHandler.SaveObject>();

        for (int i = 0; i < PuzzleManager.Instance.AllPuzzleList.Count; i++)
        {
            puzzleObjectList.Add(PuzzleManager.Instance.AllPuzzleList[i].GetSaveObject());
        }

        for (int i = 0; i < manager.allCatList.Count; i++)
        {
            catSaveObjectList.Add(manager.allCatList[i].GetSaveObject());
        }

        SaveData saveData = new SaveData
        {
            RestSaveObject = RestSaveObject,
            CastleSaveObject = CastleSaveObject,
            MatingSaveObject = MatingSaveObject,
            LightHouseSaveObject = LightHouseSaveObject,
            MilitarySaveObject = MilitarySaveObject,

            managerSaveObject = managerSaveObject,
            cycleManagerSaveObject = cycleManagerSaveObject,
            olayIsleyiciSaveObject = olayIsleyiciSaveObject,
            gameEventSaveObject = gameEventSaveObject,

            catObjectSaveObjectArray = catSaveObjectList.ToArray(),
            PuzzleSaveObjectArray = puzzleObjectList.ToArray()
        };


        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString("SystemSave", json);
        SaveLoadSystem.Save("SystemSave", json, false);
        Debug.Log("Saved!");
    }

    public void OnLoad()
    {
        string json = PlayerPrefs.GetString("SystemSave");
        json = SaveLoadSystem.Load("SystemSave");

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        Rest.Instance.SetSaveObject(saveData.RestSaveObject);
        Castle.Instance.SetSaveObject(saveData.CastleSaveObject);
        Mating.Instance.SetSaveObject(saveData.MatingSaveObject);
        LightHouse.Instance.SetSaveObject(saveData.LightHouseSaveObject);
        Military.Instance.SetSaveObject(saveData.MilitarySaveObject);

        manager.SetSaveObject(saveData.managerSaveObject);
        CycleManager.Instance.SetSaveObject(saveData.cycleManagerSaveObject);
        OlayIsleyiciScript.Instance.SetSaveObject(saveData.olayIsleyiciSaveObject);
        GameEventManager.Instance.SetSaveObject(saveData.gameEventSaveObject);

        foreach (var item in saveData.catObjectSaveObjectArray)
        {
            
            Cat cat = Cat.CatCreate(manager.catObjectPrefab, item.catPosition, item.enerjiMiktari,
                manager.catEnerjiBarPrefab, manager.EnerjiBarTransformBul(item.catArea), manager.kedilerParentTransform);
            cat.SetSaveObject(item);
            manager.allCatList.Add(cat);
        }




        for (int i = 0; i < PuzzleManager.Instance.AllPuzzleList.Count; i++)
        {
            for (int j = 0; j < saveData.PuzzleSaveObjectArray.Length; j++)
            {
                if (PuzzleManager.Instance.AllPuzzleList[i].PuzzleIndex == saveData.PuzzleSaveObjectArray[j].PuzzleIndex)
                    PuzzleManager.Instance.AllPuzzleList[i].SetSaveObject(saveData.PuzzleSaveObjectArray[j]);

            }
        }

        Debug.Log("Load!");
    }

}

[System.Serializable]
public class SaveData
{
    public Rest.SaveObject RestSaveObject = null;
    public Castle.SaveObject CastleSaveObject = null;
    public Mating.SaveObject MatingSaveObject = null;
    public LightHouse.SaveObject LightHouseSaveObject = null;
    public Military.SaveObject MilitarySaveObject = null;

    public BuildManager.SaveObject managerSaveObject = null;
    public CycleManager.SaveObject cycleManagerSaveObject = null;
    public OlayIsleyiciScript.SaveObject olayIsleyiciSaveObject = null;
    public GameEventManager.SaveObject gameEventSaveObject = null;

    public Cat.SaveObject[] catObjectSaveObjectArray;

    public PuzzleHandler.SaveObject[] PuzzleSaveObjectArray = null;

}