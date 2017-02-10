using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControllerLeft : MonoBehaviour {

    public OVRInput.Controller controller;
    public GameObject buttonsWindow;
    public GameObject virtualHand;
    public GameObject rayCasterRender;

    private void Start()
    {
        buttonsWindow.SetActive(false);
        virtualHand.SetActive(false);
        rayCasterRender.GetComponent<LineRenderer>().enabled = true;
    }

	// Update is called once per frame
	void Update () {
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);

        if(controller == OVRInput.Controller.LTouch)
        {
            if (OVRInput.GetDown(OVRInput.Button.One, controller))
            {
                print(controller.ToString() + ": One");
                virtualHand.SetActive(true);
                rayCasterRender.GetComponent<LineRenderer>().enabled = false;
            }

            if (OVRInput.GetDown(OVRInput.Button.Two, controller))
            {
                virtualHand.SetActive(false);
                rayCasterRender.GetComponent<LineRenderer>().enabled = true;
                print(controller.ToString() + ": Two");
            }

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0)
            {
                //print(controller.ToString() + ": Trigger on");
            }

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) <= 0)
            {
                //print(controller.ToString() + ": Trigger off");
            }

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0)
            {
                //print(controller.ToString() + ": Grip on");
                buttonsWindow.SetActive(true);
            }

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) <= 0)
            {
                //print(controller.ToString() + ": Grip off");
                buttonsWindow.SetActive(false);
            }
        }
    }
}
