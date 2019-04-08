using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour {
    // speed of the enemy    
    public float speed = 3;

    // range of movement
    public float rangeY = 2;

    // enemy voice
    public AudioSource voice;

    // direction
    int direction = 1;

    Vector3 initialPos;

    Transform player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Use this for initialization
    void Start()
    {
        /// save initial position
        initialPos = transform.position;
        voice.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // ternary
        float factor = (direction == 1) ? 1.2f : 1;

        //how much are we moving?
        // Time.deltaTime, speed
        float movementY = factor * speed * Time.deltaTime * direction;

        // new position y
        float newY = transform.position.y + movementY;

        // checking wether we've left our range
        if (Mathf.Abs(newY - initialPos.y) > rangeY)
        {
            direction *= -1;
            Vector3 flameDirection = new Vector3(180, 0, 0);
            gameObject.transform.GetChild(0).transform.Rotate(flameDirection);
            Vector3 flamePosition = new Vector3(0, (direction == 1) ? 0f : 0.294f);
            gameObject.transform.GetChild(0).transform.localPosition = flamePosition;
        }
        // if we can move further, let's move further!
        else
        {
            transform.position += new Vector3(0, movementY, 0);
        }
        //to always look at player for creepy purposes
        transform.LookAt(player);
        
    }
}
