using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrapController : MonoBehaviour {

    bool shouldFall;

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            shouldFall = true;
        }
    }

    //Move walls
    void MoveWalls()
    {
        var Wall1 = gameObject.transform.Find("MovingWall1");
        var Wall2 = gameObject.transform.Find("MovingWall2");
        if (Wall1.localPosition.z > 0.75f)
        {
            Wall1.localPosition += Vector3.back * Time.deltaTime;
        }
        if (Wall2.localPosition.z < -1.0f)
        { 
            Wall2.localPosition += Vector3.forward * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update () {
        if (shouldFall)
        {
            print("yeahhhh!");
            print(gameObject.transform.GetChild(1).transform.position);
            MoveWalls();
        }
    }
}
