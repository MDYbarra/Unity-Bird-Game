using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Score : NetworkBehaviour
{
   // public Text scoreText;
    public int maxScore = 5;


    [SyncVar]
    public int score;
 
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        //scoreText.text = "Score: " + score; moved to scoreText.cs
    }

    [ClientRpc]
    void RpcCurrentScore(int amount)
    {
        Debug.Log("CurrentScore:" + amount);
    }

    [ClientRpc]
    void RpcYouWin()
    {
        if (isLocalPlayer)
        {
            SceneManager.LoadScene("Win");

        }
        else
        {
            SceneManager.LoadScene("Lose");
        }
    }

    public void AddPoint()
    {
        if(!isServer){
			return;
		}
        score++;
        RpcCurrentScore(score);
        if(score >= maxScore)

        {

            RpcYouWin();

            //probably want to do an event here to make other players lose.
        }
 
        //if (score != maxScore)
            //scoreText.text = "Score: " + score;
      //  else
            //scoreText.text = "You won!";
    }
}