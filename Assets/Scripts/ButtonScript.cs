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

    public void QuitGame()
    {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
         Application.Quit();
#elif (UNITY_WEBGL)
          Application.OpenURL("blank");
#endif
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
