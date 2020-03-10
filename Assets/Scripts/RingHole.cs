using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHole : MonoBehaviour
{
    // public Score scoreManager;
    public AudioSource tada;
    public GameObject ring, ringTarget,daylight,nightlight, t_lights;


    

    private void OnCollisionEnter(Collision collision)
    {
        //scoreManager.AddPoint();

        Debug.Log("Collision Detected");
        var hit = collision.gameObject;




        var hitObject = hit.GetComponent<CharacterController>();
        if (hitObject != null)
        {
            daylight.SetActive(true);
            nightlight.SetActive(false);
            t_lights.SetActive(false);
            ringTarget.SetActive(false);
            ring.SetActive(false);
            tada.Play();
            
            Destroy(gameObject);// make sure hit object is bullet, if so destroy this target.
            return;
        }
        //Destroy(collision.gameObject);
    }
}