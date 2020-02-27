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

    public GameObject gold_vid;
    public GameObject silver_vid;
    public GameObject bronxe_vid;

    public GameObject Linewall;
    private UnityEngine.Video.VideoPlayer video;
    private Canvas can;
    private bool played;


    // Start is called before the first frame update
    void Start()
    {
        /**
        switch (check_goal.medal_index)
        {
            case 0: bronxe.GetComponent<Canvas>().enabled = true; GameObject.Find("txt_scoreB").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); bronxe_vid.GetComponent<UnityEngine.Video.VideoPlayer>().Play(); Linewall.GetComponent<MeshRenderer>().enabled = true; break;
            case 1: silver.GetComponent<Canvas>().enabled = true; PlayerPrefs.SetInt("unlockStages", check_goal.index + 1); GameObject.Find("txt_scoreS").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); silver_vid.GetComponent<UnityEngine.Video.VideoPlayer>().Play(); Linewall.GetComponent<MeshRenderer>().enabled = true; break;
            case 2: gold.GetComponent<Canvas>().enabled = true; PlayerPrefs.SetInt("unlockStages", check_goal.index + 1); GameObject.Find("txt_scoreG").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); gold_vid.GetComponent<UnityEngine.Video.VideoPlayer>().Play(); Linewall.GetComponent<MeshRenderer>().enabled = true; break;
        }
    **/
        played = false;
        switch (check_goal.medal_index)
        {
            case 0:  GameObject.Find("txt_scoreB").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); playVideo(bronxe_vid.GetComponent<UnityEngine.Video.VideoPlayer>(), bronxe.GetComponent<Canvas>()); Linewall.GetComponent<MeshRenderer>().enabled = true; break;
            case 1:  PlayerPrefs.SetInt("unlockStages", check_goal.index + 1); GameObject.Find("txt_scoreS").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); playVideo(silver_vid.GetComponent<UnityEngine.Video.VideoPlayer>(), silver.GetComponent<Canvas>()); Linewall.GetComponent<MeshRenderer>().enabled = true; break;
            case 2:  PlayerPrefs.SetInt("unlockStages", check_goal.index + 1); GameObject.Find("txt_scoreG").GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("score").ToString(); save_medal(check_goal.medal_index); playVideo(gold_vid.GetComponent<UnityEngine.Video.VideoPlayer>(), gold.GetComponent<Canvas>()); Linewall.GetComponent<MeshRenderer>().enabled = true; break;
        }
    }

    private void playVideo (UnityEngine.Video.VideoPlayer v, Canvas c)
    {
        video = v;
        can = c;
        video.Prepare();
    }

    private void Update()
    {
        checkPreparing();
    }

    private void checkPreparing()
    {
        if (video.isPrepared && played == false)
        {
            played = true;
            video.Play();
            can.enabled = true;
        }
    }

    private void save_medal(int medalIndex)
    {
        try {
            if (check_goal.index>=0) {
                string stageName = "stage" + check_goal.index;
                int tmp_medalIndex = PlayerPrefs.GetInt(stageName);
                if (medalIndex >= tmp_medalIndex)
                {
                    PlayerPrefs.SetInt(stageName, medalIndex+1);
                } else
                {
                    
                    if (medalIndex == 0 && tmp_medalIndex==0)
                    {
                        PlayerPrefs.SetInt(stageName, medalIndex+1);
                    }
                }

            }
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        

    }


}
 
