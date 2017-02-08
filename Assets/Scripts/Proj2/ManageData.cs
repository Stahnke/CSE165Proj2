using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManageData : MonoBehaviour {

    ArrayList objects;
    public GameObject[] prefabs;

    void Start()
    {
        objects = new ArrayList();
        LoadData("myTest.txt");
        SaveData("myTest.txt");
    }

    void SaveData(string filename)
    {
        print("Saving Data");

        //Set up path
        string path = Application.dataPath + "/Resources/Files/" + filename;

        ArrayList allLines = new ArrayList();
        float xpos, ypos, zpos;
        float xrot, yrot, zrot;
        GameObject curObj;

        //
        for(int x = 0; x < objects.Count; x++)
        {
            curObj = (GameObject)objects.ToArray()[x];

            //Get pos
            xpos = curObj.transform.position.x;
            ypos = curObj.transform.position.y;
            zpos = curObj.transform.position.z;

            //***FIX THIS***//
            //Get rot
            xrot = curObj.transform.rotation.x;
            yrot = curObj.transform.rotation.y;
            zrot = curObj.transform.rotation.z;


            print(curObj.name);
            print(xpos + " " + ypos + " " + zpos);
            print(xrot + " " + yrot + " " + zrot);

            string line = "6" + "," + xpos + "," + ypos + "," + zpos + "," + xrot + "," + yrot + "," + zrot;
            allLines.Add(line);
        }

        System.IO.File.WriteAllLines(path, (string[])allLines.ToArray(typeof(string)));
    }

    void LoadData(string filename)
    {
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
            Quaternion rotation = Quaternion.Euler(xrot, yrot, zrot);

            //Instantiate
            GameObject curObj = Instantiate(curPrefab, position, rotation) as GameObject;
            objects.Add(curObj);
        }

        file.Close();
    }
}
