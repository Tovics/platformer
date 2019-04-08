using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoManager : MonoBehaviour {

    public Text instruction;
    bool starting = true;
    bool leftDone = false;
    bool rightDone = false;
    bool backDone = false;
    bool forwardDone = false;
    // Use this for initialization
    void Start () {
        instruction.text = "Press 'a' to turn left!";
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && starting)
        {
            leftDone = true;
            starting = false;
            instruction.text = "Press 'd' to turn right!";
        }
        if (Input.GetKeyDown("d") && leftDone)
        {
            rightDone = true;
            leftDone = false;
            instruction.text = "Press 's' to go backwards!";
        }
        if (Input.GetKeyDown("s") && rightDone)
        {
            backDone = true;
            rightDone = false;
            instruction.text = "Press 'w' to go forward!";
        }
        if (Input.GetKeyDown("w") && backDone)
        {
            forwardDone = true;
            backDone = false;
            instruction.text = "Press 'space' once to jump!";
        }
        if (Input.GetKeyDown("space") && forwardDone)
        {
            forwardDone = false;
            instruction.text = "Follow the BLUEISH light to find the crystal!";
        } 
    }
}
