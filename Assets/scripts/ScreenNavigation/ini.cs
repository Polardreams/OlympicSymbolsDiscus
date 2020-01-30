using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ini : MonoBehaviour
{
    // Start is called before the first frame update

    public bool fromStage = false;
    void Start()
    {
        int unlockStages = PlayerPrefs.GetInt("unlockStages");
        for (int n = 2; n<unlockStages+1; n++ )
        {
            GameObject.Find("btn_level_" + n.ToString()).GetComponent<Button>().interactable = true;
        }

        if (PlayerPrefs.GetInt("nav_fromStage") == 1)
        {
            GameObject.Find("TitleScreen").GetComponent<Canvas>().enabled = false;
            GameObject.Find("LevelProductionScreen").GetComponent<Canvas>().enabled = true;
            PlayerPrefs.SetInt("nav_fromStage", 0);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
