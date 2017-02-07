using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour {

    public Transform spawnerParentTrans;
    public Transform spawnerChildTrans;
    public GameObject brickPrefab;
    public GameObject brickParentPrefab;
    public int HEIGHT = 20;
    private const float ROTATION = 7.5f;
    private const int CIRCLE_DEG = 360;
    private Vector3 startPos;

	// Use this for initialization
    void Start()
    {
        startPos = gameObject.transform.position;
        SpawnBricks();
    }

	public void SpawnBricks () {

        gameObject.transform.position = startPos;

        GameObject brickParent = Instantiate(brickParentPrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0));

        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < CIRCLE_DEG / ROTATION; x++)
            {
                GameObject brick = Instantiate(brickPrefab, spawnerChildTrans.position, spawnerParentTrans.rotation);
                brick.transform.parent = brickParent.transform;
                Color brickColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0, 1.0f));
                brick.GetComponent<Renderer>().material.color = brickColor;
                spawnerParentTrans.Rotate(0.0f, ROTATION, 0.0f, Space.Self);
                
            }
            spawnerParentTrans.Translate(0.0f, brickPrefab.transform.localScale.y, 0.0f);
            spawnerParentTrans.Rotate(0.0f, ROTATION / 2, 0.0f, Space.Self);
            
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
