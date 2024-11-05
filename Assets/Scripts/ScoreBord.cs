using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public int score;
    public string userName;
}

public class ScoreBord : Singleton<ScoreBord>
{
    public static ScoreBord instance;

    private SaveData saveData;
    [SerializeField]
    private List<ScoreEntry> entries;


   override public void Awake()
    {
        base.Awake();
        //LoadGame();
      //  instance = this;
        saveData = new SaveData();
        
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(string name, int score)
    {

      for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].userName == name)
            {
                Debug.Log("entery deleted: " + entries[i].userName);
                entries.RemoveAt(i);
            }
        }



       // SaveGame();
    }
    [ContextMenu("Add Test Scores")]
    public void AddTestScores(string name, int score) 
    {
        name = "Test";
        score = Random.Range(1, 10000000);

        for (int i = 0;i < 100;i++) 
        {
        ScoreEntry entry = new ScoreEntry();
        entry.score = Random.Range(1,100000000);
        entry.userName = "Test" + i;
        entries.Add(entry);
        }
       // SaveGame();
    }

    [ContextMenu("Reset")]
    public void Reset()
    {
        entries = new List<ScoreEntry>();
       // SaveGame();
    }

    

    public List<ScoreEntry> GetEntries()
    {
        return entries;
    }

/*    public void LoadGame()
    {

        saveData = SaveSystem.DeSerializeData();
        if (saveData == null)
        {
            saveData = new SaveData();
        }

        entries = saveData.entries;
    }

    public void SaveGame()
    {
        saveData.entries = entries;

        SaveSystem.SerializeData(saveData);
    }*/










}
