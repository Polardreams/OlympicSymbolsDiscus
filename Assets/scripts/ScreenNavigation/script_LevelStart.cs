using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_LevelStart : MonoBehaviour
{
    // Start is called before the first frame update

    public void ScreenToLevel_1 ()
    {
        SceneManager.LoadScene("stage 1", LoadSceneMode.Single);
    }

    public void ScreenToLevel_2()
    {
        SceneManager.LoadScene("stage 2", LoadSceneMode.Single);
    }

    public void ScreenToLevel_3()
    {
        SceneManager.LoadScene("stage 3", LoadSceneMode.Single);
    }

    public void ScreenToLevel_4()
    {
        SceneManager.LoadScene("stage 4", LoadSceneMode.Single);
    }

    public void ScreenToLevel_5()
    {
        SceneManager.LoadScene("stage 5", LoadSceneMode.Single);
    }

    public void ScreenToLevel_6()
    {
        SceneManager.LoadScene("stage 6", LoadSceneMode.Single);
    }

    public void ScreenToLevel_7()
    {
        SceneManager.LoadScene("stage 7", LoadSceneMode.Single);
    }

    public void ScreenToLevel_8()
    {
        SceneManager.LoadScene("stage 8", LoadSceneMode.Single);
    }

    public void ScreenToLevel_9()
    {
        SceneManager.LoadScene("stage 9", LoadSceneMode.Single);
    }

    public void ScreenToLevel_10()
    {
        SceneManager.LoadScene("stage 10", LoadSceneMode.Single);
    }

    public void next_level ()
    {
        SceneManager.LoadScene("stage " + (PlayerPrefs.GetInt("unlockStages")+1).ToString(), LoadSceneMode.Single);
    }

    public void repeat_level ()
    {
        SceneManager.LoadScene("stage " + PlayerPrefs.GetInt("unlockStages").ToString(), LoadSceneMode.Single);
    }

}
