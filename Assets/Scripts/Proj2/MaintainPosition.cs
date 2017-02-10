using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainPosition : MonoBehaviour {

    private float x;
    private float y;
    private float z;

    public float xPos = 0;
    public float yPos = 0;
    public float zPos = 0;

    public bool xLock = false;
    public bool yLock = false;
    public bool zLock = false;

    private bool selected = false;

    private void Start()
    {
        xPos = gameObject.transform.position.x;
        yPos = gameObject.transform.position.y;
        zPos = gameObject.transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {

        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;

        if (xLock) x = xPos;
        if (yLock) y = yPos;
        if (zLock) z = zPos;

        gameObject.transform.position = new Vector3(x, y, z);

	}

    public void SendSelected(bool selected)
    {
        this.selected = selected;
    }
}
