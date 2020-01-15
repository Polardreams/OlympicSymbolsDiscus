using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_score : MonoBehaviour
{
    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        if (score == null)
        {
            score = 0;
            PlayerPrefs.SetInt("score",0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int get_score ()
    {
        return PlayerPrefs.GetInt("score");
    }

}
