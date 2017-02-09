using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainRotation : MonoBehaviour {

    public float xRot = 0;
    public float yRot = 0;
    public float zRot = 0;

    public bool xLock = false;
    public bool yLock = false;
    public bool zLock = false;

    private bool selected = false;

    private GameObject rightHand;

    private float curZ = 0;
    private float previousZ = 0;

    // Use this for initialization
    void Start () {
        rightHand = GameObject.Find("RightHand");
        //previousZ = rightHand.transform.eulerAngles.z;
    }
	
	// Update is called once per frame
	void Update () {

        float x = gameObject.transform.eulerAngles.x;
        float y = gameObject.transform.eulerAngles.y;
        float z = gameObject.transform.eulerAngles.z;

        if (xLock) x = xRot;
        if (yLock) y = yRot;
        if (zLock) z = zRot;

        if (selected)
        {
            curZ = rightHand.transform.eulerAngles.z;
            if (!xLock) x = (curZ);
            if (!yLock) y = (curZ);
            if (!zLock) z = (curZ);
            //print(curZ - previousZ);
        }

        gameObject.transform.eulerAngles = new Vector3(x,y,z);

        //previousZ = curZ;
	}

    public void SendSelected(bool selected)
    {
        this.selected = selected;
    }
}
