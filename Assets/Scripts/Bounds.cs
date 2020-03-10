using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    // public Score scoreManager;
    public AudioSource wall;
   




    private void OnCollisionEnter(Collision collision)
    {
        //scoreManager.AddPoint();

        Debug.Log("Collision Detected");
        var hit = collision.gameObject;




        var hitObject = hit.GetComponent<CharacterController>();
        if (hitObject != null)
        {

            wall.Play();
           // Destroy(gameObject);// make sure hit object is bullet, if so destroy this target.
            return;
        }
        //Destroy(collision.gameObject);
    }
}