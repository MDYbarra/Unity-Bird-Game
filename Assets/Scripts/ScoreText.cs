using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreText : NetworkBehaviour
{
    
    Score score;
    

    void Awake()
    {
        score = GetComponent<Score>();
    }

    void OnGUI()
    {

        // put box in top right with score
        if(!isLocalPlayer)
        {
            return;
        }

        GUI.TextField(new Rect(Screen.width-100, 20, 100, 30), "Score: "+ score.score);
        
    }
 

}
