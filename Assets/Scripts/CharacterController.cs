using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterController : NetworkBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 90;
    public float force = 1;
    public float maxSpeed = 20;
    public GameObject bullet;
    public GameObject drop;
    public Camera fpsCam;
    public AudioSource flying;
    public AudioSource shoot;
    public GameObject wing1, wing2;
    private int counter = 0;
    private int count = 0;

    Rigidbody rb;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        if (!isLocalPlayer)
        {
            fpsCam.enabled = false;
            return;
        }
        else
        {
            //enable cam for the local player
            fpsCam.enabled = true;

        }


    }



    // command
    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        var newbullet = (GameObject)Instantiate(
             bullet,
             transform.position + transform.forward,
             Quaternion.identity);
        newbullet.GetComponent<Bullet>().playerScore = GetComponent<Score>();


        newbullet.GetComponent<Rigidbody>().velocity = transform.forward * 40;

        // spawn the bullet on the clients
        NetworkServer.Spawn(newbullet);

        shoot.Play();

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients 
        Destroy(newbullet, 2.0f);
    }

    [Command]
    void CmdDrop()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally
        var newbullet = (GameObject)Instantiate(
             drop,
             transform.position - transform.up,
             Quaternion.identity);
        newbullet.GetComponent<Bullet>().playerScore = GetComponent<Score>();


        newbullet.GetComponent<Rigidbody>().velocity = -4*transform.up;

        // spawn the bullet on the clients
        NetworkServer.Spawn(newbullet);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients 
        Destroy(newbullet, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // if not local player don't run this
        if (!isLocalPlayer)
        {

            return;
        }
        //set wings to standard position
        wing1.SetActive(true);

        wing2.SetActive(false);


        //move foward
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += this.transform.forward * speed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.S)) //move backward
        {
            rb.velocity -= this.transform.forward * speed * Time.deltaTime;
            
        }

        if (rb.velocity.magnitude > maxSpeed) //don't go over a maxSpeed
            rb.velocity = rb.velocity.normalized * maxSpeed;

        if (Input.GetKey(KeyCode.D)) //turn left/right
            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.A))
            t.rotation *= Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);



        // flying this makes the bird flap its wings
        if (Input.GetKeyDown(KeyCode.O))
        {

            rb.AddForce(t.up * force / 2);
            //flap wings
            wing1.SetActive(false);
            wing2.SetActive(true);
            flying.Play();  // add sound when bird flaps wings

        }


        // shoots bullets from birds mouth
        if (Input.GetKeyDown(KeyCode.K))
        {
            CmdFire();
            // GameObject newBullet = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
            //newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * 5;
            //newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
            //shoot.Play(); // add sound when bird shoots
            //Destroy(newBullet, 5);

        }

        // drops poop
        if (Input.GetKeyDown(KeyCode.P))
        {
            CmdDrop();
        }


       

        // Quits the game
        if (Input.GetKey(KeyCode.Y))
        {
            Application.Quit();
        }




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
            //add a point to the player 
            GetComponent<Score>().AddPoint();
        }
    }
}