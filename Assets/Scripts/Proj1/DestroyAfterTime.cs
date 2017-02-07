using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float waitTime = 2.0f;

	// Use this for initialization
	void Start () {

        Invoke("destroy", waitTime);
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
