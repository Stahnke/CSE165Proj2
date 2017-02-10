using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualHand : MonoBehaviour {

    GameObject otherObject;

    public GameObject emptyLocation;

    private bool selected = false;
    private GameObject selectedObject;


    private void OnTriggerEnter(Collider other)
    {
        otherObject = other.gameObject;
        print("Selected: " + otherObject.name);

    }

    private void OnTriggerExit(Collider other)
    {
        selectedObject = null;
    }

    public bool Launch() {

        if (selected == true)
        {
            selected = false;
            Rigidbody[] rigidbodies = selectedObject.GetComponentsInChildren<Rigidbody>();
            Collider[] colliders = selectedObject.GetComponentsInChildren<Collider>();
            Light[] lights = selectedObject.GetComponentsInChildren<Light>();
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].isKinematic = false;
                //rigidbodies[i].GetComponent<MaintainRotation>().SendSelected(false);
            }

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = true;
            }

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = false;
            }

            selectedObject.transform.DetachChildren();
            Destroy(selectedObject);
            selectedObject = null;
            otherObject = null;
        }

        else
        {
            if (otherObject != null)
            {
                print("Raycast detect object: " + otherObject.name);

                if (otherObject.CompareTag("labObject"))
                {
                    GameObject emptyObject = Instantiate(emptyLocation, gameObject.transform.position, this.transform.parent.rotation) as GameObject;
                    emptyObject.transform.SetParent(this.transform.parent);
                    emptyObject.GetComponent<MaintainWorldRotation>().SendSelected(true);
                    selectedObject = emptyObject;
                    otherObject.transform.SetParent(selectedObject.transform);
                    otherObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                    otherObject.transform.GetComponent<Collider>().enabled = false;
                    selected = true;
                    return true;
                }
            }
        }
        return false;
    }
}
