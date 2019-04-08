using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingColumnLava : MonoBehaviour {
    bool shouldFall = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldFall = true;
        }
    }

    void Update()
    {
        if (shouldFall)
        {
            if (gameObject.transform.rotation.x < 50)
            {
                Vector3 fallDirection = new Vector3(1, 0, 1);
                gameObject.transform.Rotate(fallDirection, 15f * Time.deltaTime);
            }
        }
    }
}
