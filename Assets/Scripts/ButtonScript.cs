using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void Play()
    {
        //ScoreBord.instance.LoadGame();
        SceneManager.LoadScene("main");
    }
}
