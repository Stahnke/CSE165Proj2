using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManageData : MonoBehaviour {

    public GameObject[] prefabs;
    private ArrayList objects  = new ArrayList();
    private ArrayList allLines = new ArrayList();

    public GameObject saver;

    private void Start()
    {
        /*Transform[] objects = saver.GetComponentsInChildren<Transform>();
        for(int x = 0; x < objects.Length; x++)
        {
            AddObject(objects[x].gameObject);
        }
        SaveData("myTest.txt");*/
        LoadData("myTest.txt");
    }

    public void SaveData(string filename)
    {
        //Empty the lines to write to the file
        allLines.Clear();

        //Begin saving data
        print("Saving Data");

        //Set up path
        string path = Application.dataPath + "/Resources/Files/" + filename;

        float xpos, ypos, zpos;
        float xrot, yrot, zrot;
        GameObject curObj;

        //For each object in our array, get the data and write it
        for(int i = 0; i < objects.Count; i++)
        {
            curObj = (GameObject)objects.ToArray()[i];

            //Get pos
            xpos = curObj.transform.position.x;
            ypos = curObj.transform.position.y;
            zpos = curObj.transform.position.z;

            //***FIX THIS***//
            //Get rot
            xrot = curObj.transform.eulerAngles.x;
            yrot = curObj.transform.eulerAngles.y;
            zrot = curObj.transform.eulerAngles.z;

            print(curObj.name);
            print(xpos + " " + ypos + " " + zpos);
            print(xrot + " " + yrot + " " + zrot);

            string line = curObj.name + "," + xpos + "," + ypos + "," + zpos + "," + xrot + "," + yrot + "," + zrot;
            allLines.Add(line);
        }

        //Write all lines to file
        System.IO.File.WriteAllLines(path, (string[])allLines.ToArray(typeof(string)));
    }

    public void LoadData(string filename)
    {
        //Delete all the objects currently on the scene
        print("Deleting scene");

        while(objects.Count > 0)
        {
            GameObject.Destroy((GameObject)objects[0]);
            objects.RemoveAt(0);
        }
        
        //Begin loading process
        print("Loading Data");

        //Set up path
        string path = Application.dataPath + "/Resources/Files/" + filename;

        string line;
        int objectNum;
        float xpos, ypos, zpos;
        float xrot, yrot, zrot;
        GameObject curPrefab;

        //Open file
        System.IO.StreamReader file = new System.IO.StreamReader(path);

        //Read file
        while((line = file.ReadLine()) != null)
        {
            if(line != "\n" && line != "")
            {
                //READ FROM FILE
                List<float> numbers = new List<float>(Array.ConvertAll(line.Split(','), float.Parse));

                //Set all values
                objectNum = (int)numbers[0];
                xpos = numbers[1];
                ypos = numbers[2];
                zpos = numbers[3];
                xrot = numbers[4];
                yrot = numbers[5];
                zrot = numbers[6];
                curPrefab = prefabs[objectNum];

                //Load into Vector3 and Quaternion
                Vector3 position = new Vector3(xpos, ypos, zpos);
                Vector3 rotation = new Vector3(xrot, yrot, zrot);

                //Instantiate
                GameObject curObj = Instantiate(curPrefab, position, Quaternion.identity) as GameObject;
                curObj.transform.eulerAngles = rotation;
                curObj.name = objectNum + "";
                objects.Add(curObj);
            }
        }
        file.Close();
    }

    public void AddObject(GameObject curObj)
    {
        objects.Add(curObj);
    }
}
