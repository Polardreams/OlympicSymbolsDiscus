using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class receiveResult : MonoBehaviour
{
    public GameObject gold;
    public GameObject silver;
    public GameObject bronxe;


    // Start is called before the first frame update
    void Start()
    {

        switch (check_goal.medal_index)
        {
            case 0: bronxe.GetComponent<Canvas>().enabled = true; GameObject.Find("txt_scoreB").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); break;
            case 1: silver.GetComponent<Canvas>().enabled = true; PlayerPrefs.SetInt("unlockStages", check_goal.index + 1); GameObject.Find("txt_scoreS").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); break;
            case 2: gold.GetComponent<Canvas>().enabled = true; PlayerPrefs.SetInt("unlockStages", check_goal.index + 1); GameObject.Find("txt_scoreG").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); break;
        }
    }

    private void save_medal(int medalIndex)
    {
        try {
            if (check_goal.index>0) {
                string stageName = "stage" + check_goal.index;
                int tmp_medalIndex = PlayerPrefs.GetInt(stageName);
                if (medalIndex >= tmp_medalIndex)
                {
                    PlayerPrefs.SetInt(stageName, medalIndex);
                }
            }
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        

    }


}
 
