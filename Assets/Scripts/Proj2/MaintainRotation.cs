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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float x = gameObject.transform.eulerAngles.x;
        float y = gameObject.transform.eulerAngles.y;
        float z = gameObject.transform.eulerAngles.z;

        if (xLock) x = xRot;
        if (xLock) y = yRot;
        if (xLock) z = zRot;

        gameObject.transform.eulerAngles = new Vector3(x,y,z);


	}
}
