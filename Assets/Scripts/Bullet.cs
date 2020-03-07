using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    public Score playerScore;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (!isServer)
        {
            return;
        }

        var hit = collision.gameObject;
        var hitObject = hit.GetComponent<Target>();
        if (hitObject != null)
        {
            //add a point to the player that shot the target if it exists
            playerScore.AddPoint();
            //destroy the bullet
            Destroy(gameObject);
        }
    }
}
