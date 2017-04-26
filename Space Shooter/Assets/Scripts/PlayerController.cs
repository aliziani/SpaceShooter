using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    private float xShotPosition = 0.168f;
    private float yShotPosition = 0.06f;
    private float zShotPosition = 0.576f;

    /// <summary>
    /// Called before each physics step
    /// </summary>
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody rb = GetComponent<Rigidbody>();

        var movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);
        rb.velocity = movement;
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler(rb.velocity.z , 0.0f, rb.velocity.x * -tilt);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            // generate right laser
            var vectorShot = new Vector3(shotSpawn.position.x + xShotPosition, shotSpawn.position.y /*+ yShotPosition*/, shotSpawn.position.z);
            Instantiate(shot, vectorShot, shotSpawn.rotation);
            vectorShot.x = shotSpawn.position.x - xShotPosition;
            //vectorShot.y = shotSpawn.position.y - 0.456f;
            Instantiate(shot, vectorShot, shotSpawn.rotation);
            // let me sing the song of my people
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }
    
}
