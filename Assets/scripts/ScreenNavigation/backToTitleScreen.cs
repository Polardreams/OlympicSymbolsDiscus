using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToTitleScreen : MonoBehaviour
{
public void backToTitle ()
    {

        PlayerPrefs.SetInt("nav_fromStage",1);
        SceneManager.LoadScene("Screens", LoadSceneMode.Single);
    }
}
