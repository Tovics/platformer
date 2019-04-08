using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLavaPlatform : MonoBehaviour {
    public bool shouldMove = false;
    public float range = 2;
    public float speed = 1;
    public bool direction = true;
    public string Axis = "x";
    int localDirection = 1;
    Vector3 initialPos;
    Vector3 scale;
    public AudioSource StartUpSound;
    public AudioSource StartSound;
    public AudioSource StopSound;
    public AudioSource LoopSound;
    bool makeSound = false;

    void Start()
    {
        /// save initial position
        initialPos = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            makeSound = true;
            scale = other.transform.localScale;
            other.transform.parent = transform;
            other.transform.localScale = scale;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            makeSound = false;
            other.transform.parent = null;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (!shouldMove)
            {
                if (!StartUpSound.isPlaying)
                {
                    StartUpSound.Play();
                }
                shouldMove = true;
            }
        }
    }

    void Update()
    {
        if (!makeSound)
        {
            StartSound.Stop();
            LoopSound.Stop();
            StartUpSound.Stop();
            StopSound.Stop();
        }
        if (shouldMove)
        {
            if (Axis == "x")
            {
                MoveX();
            }
            else if (Axis == "y")
            {
                MoveY();
            }
            else if (Axis == "z")
            {
                MoveZ();
            }
        }
    }

    void MoveY()
    {
        float movement = speed * Time.deltaTime * localDirection;
        if (!direction) { movement *= -1; }
        float newPosY = transform.position.y + movement;
        if (direction)
        {
            
            if (newPosY >= initialPos.y + range
                || newPosY < initialPos.y)
            {
                if (makeSound)
                {
                    LoopSound.Stop();
                    StartSound.Play();
                }
                localDirection *= -1;
            }
            else
            {
                if (makeSound && !StartSound.isPlaying && !LoopSound.isPlaying)
                {
                        LoopSound.Play();
                }
                transform.position += new Vector3(0, movement, 0);
            }
        }
        else
        {
            if (newPosY < initialPos.y - range
                || newPosY >= initialPos.y)
            {
                if (makeSound)
                {
                    LoopSound.Stop();
                    StartSound.Play();
                }
                localDirection *= -1;
            }
            else
            {
                if (makeSound && !StartSound.isPlaying && !LoopSound.isPlaying)
                {
                    LoopSound.Play();
                }
                transform.position += new Vector3(0, movement, 0);
            }
        }
    }

    void MoveX()
    {
        float movement = speed * Time.deltaTime * localDirection;
        if (!direction) { movement *= -1; }
        float newPosX = transform.position.x + movement;
        if (direction)
        {
            if (newPosX >= initialPos.x + range
                || newPosX < initialPos.x)
            {
                if (makeSound)
                {
                    LoopSound.Stop();
                    StartSound.Play();
                }
                localDirection *= -1;
            }
            else
            {
                if (makeSound && !StartSound.isPlaying && !LoopSound.isPlaying)
                {
                    LoopSound.Play();
                }
                transform.position += new Vector3(movement, 0, 0);
            }
        }
        else
        {
            if (newPosX < initialPos.x - range
                || newPosX >= initialPos.x)
            {
                if (makeSound)
                {
                    LoopSound.Stop();
                    StartSound.Play();
                }
                localDirection *= -1;
            }
            else
            {
                if (makeSound && !StartSound.isPlaying && !LoopSound.isPlaying)
                {
                    LoopSound.Play();
                }
                transform.position += new Vector3(movement, 0, 0);
            }
        }
    }

    void MoveZ()
    {
        float movement = speed * Time.deltaTime * localDirection;
        if (!direction) { movement *= -1; }
        float newPosZ = transform.position.z + movement;
        if (direction)
        {
            if (newPosZ >= initialPos.z + range
                || newPosZ < initialPos.z)
            {
                if (makeSound)
                {
                    LoopSound.Stop();
                    StartSound.Play();
                }
                localDirection *= -1;
            }
            else
            {
                if (makeSound && !StartSound.isPlaying && !LoopSound.isPlaying)
                {
                    LoopSound.Play();
                }
                transform.position += new Vector3(0, 0, movement);
            }
        }
        else
        {
            if (newPosZ < initialPos.z - range
                || newPosZ >= initialPos.z)
            {
                if (makeSound)
                {
                    LoopSound.Stop();
                    StartSound.Play();
                }
                localDirection *= -1;
            }
            else
            {
                if (makeSound && !StartSound.isPlaying && !LoopSound.isPlaying)
                {
                    LoopSound.Play();
                }
                transform.position += new Vector3(0, 0, movement);
            }
        }
    }
}
