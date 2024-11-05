using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowScorebord : MonoBehaviour
{

    [SerializeField]
    private List<TextMeshProUGUI> textBoxes;
    [SerializeField]
    private TextMeshProUGUI TextBoxNotTop;

    // Start is called before the first frame update
    void Start()
    {
        ScoreBord.instance.LoadGame();
        ShowScorebordFun();
    }

    private void ShowScorebordFun()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i >= ScoreBord.instance.GetEntries().Count)
            {
                textBoxes[i].text = "";
            }
            else
            {

                textBoxes[i].text = i + 1 + ": " + ScoreBord.instance.GetEntries()[i].score.ToString() + " " + ScoreBord.instance.GetEntries()[i].userName;

            }
        }
    }
}
