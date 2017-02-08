using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    public OVRInput.Controller controller;

	// Update is called once per frame
	void Update () {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);

        if (OVRInput.GetDown(OVRInput.Button.One, controller))
        {
            print(controller.ToString()+ ": One");
        }

        else if (OVRInput.GetDown(OVRInput.Button.Two, controller))
        {
            print(controller.ToString()+ ": Two");
        }

        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0)
        {
            print(controller.ToString()+ ": Trigger");
        }

        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0)
        {
            print(controller.ToString() + ": Grip");
        }
    }
}
