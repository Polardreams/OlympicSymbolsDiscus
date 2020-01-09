using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backToTitleScreen : MonoBehaviour
{
public void backToTitle ()
    {
        SceneManager.LoadScene("Screens", LoadSceneMode.Single);
    }
}
