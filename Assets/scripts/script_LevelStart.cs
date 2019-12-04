using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_LevelStart : MonoBehaviour
{
    // Start is called before the first frame update

    public void ScreenToLevel_1 ()
    {
        SceneManager.LoadScene("TestStage", LoadSceneMode.Single);
    }

}
