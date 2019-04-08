using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertForPlayer : MonoBehaviour {
    public string message;
    public float showTime;
    bool wasShown = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!wasShown)
            {
                GameManager.instance.hudManager.ShowMessage(message, showTime);
                wasShown = true;
            }
        }
    }
}
