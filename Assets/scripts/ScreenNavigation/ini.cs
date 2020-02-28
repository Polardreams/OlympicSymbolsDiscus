using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ini : MonoBehaviour
{
    public Sprite gold_medal;
    public Sprite silver_medal;
    public Sprite bronze_medal;

    public bool fromStage = false;
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        int unlockStages = PlayerPrefs.GetInt("unlockStages");

        ini_firstLevel();

        for (int n = 2; n<unlockStages+1; n++ )
        {
            GameObject g = GameObject.Find("btn_level_" + n.ToString());
            g.GetComponent<Button>().interactable = true;

            switch (PlayerPrefs.GetInt("stage" + n.ToString()))
            {
                case 0: g.transform.GetChild(1).GetComponent<Image>().sprite = null; break;
                case 1: g.transform.GetChild(1).GetComponent<Image>().enabled = true; g.transform.GetChild(1).GetComponent<Image>().sprite = bronze_medal; break;
                case 2: g.transform.GetChild(1).GetComponent<Image>().enabled = true; g.transform.GetChild(1).GetComponent<Image>().sprite = silver_medal; break;
                case 3: g.transform.GetChild(1).GetComponent<Image>().enabled = true; g.transform.GetChild(1).GetComponent<Image>().sprite = gold_medal;  break;
            }

        }

        if (PlayerPrefs.GetInt("nav_fromStage") == 1)
        {
            GameObject.Find("TitleScreen").GetComponent<Canvas>().enabled = false;
            GameObject.Find("LevelProductionScreen").GetComponent<Canvas>().enabled = true;
            PlayerPrefs.SetInt("nav_fromStage", 0);
        }

        
    }

    private void ini_firstLevel ()
    {
            //Sonderbehandlung
            GameObject g = GameObject.Find("btn_level_1");
            switch (PlayerPrefs.GetInt("stage1"))
            {
            case 0: g.transform.GetChild(1).GetComponent<Image>().sprite = null; break;
            case 1: g.transform.GetChild(1).GetComponent<Image>().enabled = true; g.transform.GetChild(1).GetComponent<Image>().sprite = bronze_medal; break;
            case 2: g.transform.GetChild(1).GetComponent<Image>().enabled = true; g.transform.GetChild(1).GetComponent<Image>().sprite = silver_medal; break;
            case 3: g.transform.GetChild(1).GetComponent<Image>().enabled = true; g.transform.GetChild(1).GetComponent<Image>().sprite = gold_medal; break;
        }
    }

}
