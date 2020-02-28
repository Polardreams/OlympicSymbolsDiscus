using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class repeatDialog : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject dialog;
    private void Start()
    {
        dialog = GameObject.Find("UI_repeat_dialog");
    }

    public void finishStage()
    {
        Invoke("switch_Scene", 2.5f);
        dialog.GetComponent<Canvas>().enabled = false;
    }
    private void switch_Scene()
    {
        SceneManager.LoadScene(check_goal.nxt_stage, LoadSceneMode.Single);
    }

    public void repeatStage()
    {
        //SceneManager.LoadScene("Stage "+check_goal.stat_scene_index.ToString());
        SceneManager.LoadScene("David_playground");//testzwecke
        dialog.GetComponent<Canvas>().enabled = false;
    }

}
