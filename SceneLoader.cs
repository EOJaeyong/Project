using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
       
    }

    public void LoadRD2Scene()
    {
        //  RD2 æ¿¿∏∑Œ ¿Ãµø
        SceneManager.LoadScene("RD2");
    }

    public void LoadRD1Scene()
    {
        // RD1 æ¿¿∏∑Œ ¿Ãµø
        SceneManager.LoadScene("RD1");
    }

    public void LoadRD3Scene()
    {
        // RD1 æ¿¿∏∑Œ ¿Ãµø
        SceneManager.LoadScene("RD3");
    }
    public void HappyEnding()
    {
        SceneManager.LoadScene("HappyEnding");
    }

    public void BaDEnding()
    {
        SceneManager.LoadScene("BadEnding");
    }
}
