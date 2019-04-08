using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLavaPlatform : MonoBehaviour {
    bool shouldFall = false;
    public float sinkTargetY = -4;
    public float lavaY = -1.3f;
    public AudioSource lavaSplash;
    public bool playLavaSound = true;
    public AudioSource crackSound;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            crackSound.Play();
            shouldFall = true;
        } 
            
    }

    void Update()
    {
        if (shouldFall)
        {
            if (gameObject.transform.position.y <= lavaY && playLavaSound)
            {
                crackSound.Stop();
                lavaSplash.Play();
                playLavaSound = false;
            }
            if (gameObject.transform.position.y > sinkTargetY)
            gameObject.transform.position += Vector3.down * Time.deltaTime;
        }
    }
}
