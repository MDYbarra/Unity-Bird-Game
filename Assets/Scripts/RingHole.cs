using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHole : MonoBehaviour
{
    // public Score scoreManager;
    public AudioSource pop;
    public GameObject ring, ringTarget;

    private void OnCollisionEnter(Collision collision)
    {
        //scoreManager.AddPoint();
        Debug.Log("Collision Detected");
        var hit = collision.gameObject;
        //var hitObject = hit.GetComponent<Bullet>();
        //if (hitObject != null)
        //{
        //    pop.Play();
        //    Destroy(gameObject);// make sure hit object is bullet, if so destroy this target.
        //    return;

        //}
        var hitObject2 = hit.GetComponent<CharacterController>();
        if (hitObject2 != null)
        {
            ringTarget.SetActive(false);
            ring.SetActive(false);
            pop.Play();
            Destroy(gameObject);// make sure hit object is bullet, if so destroy this target.
            return;
        }
        //Destroy(collision.gameObject);
    }
}