﻿using UnityEngine;
using UnityEngine.UI; //create public inputfield 
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;
using System;
using KeyboardPosition;


public class SeparateKeyboardCharacterCreator : KeyboardController
{
    
    int KEYS_NUMBER = 32; //how many keys are in the keyboard
    float mirrorRealDifference = 0.4f; // mirror keyboard and real keyboard height difference
    public GameObject controllerPF;
    public GameObject leftController; // left real controller
    public GameObject rightController; // right real controller
    GameObject mirrorControllerLeft; // left mirror controller
    GameObject mirrorControllerRight; // right mirror controller
    XRGrabInteractable m_InteractableBase;
    public GameObject system;
    public int segments = 10;
    public float xradius = 0.01f;
    public float yradius = 0.01f;
    public float smallerXradius;
    public float smallerYradius;
    //Prefab for a 3D key
    public GameObject PF_Key;
    public Button Button_Timer;
    public bool active = false;
    List<GameObject> points = new List<GameObject>();  // including real/lower keys and mirror/upper keys
    private KeyboardWrapper kw = new KeyboardWrapper();
    private Vector3 keyboardModelPosition;   // only include real/lower keys but not mirror/upper keys
    // Update is called once per frame
    
    private void Start()
    {

    }
    private void Update()
    {
        if (mirrorControllerLeft)
        {
            mirrorControllerLeft.transform.position = leftController.transform.position + new Vector3(0f, mirrorRealDifference, 0);
            mirrorControllerLeft.transform.rotation = leftController.transform.rotation;
        }

        if (mirrorControllerRight)
        {
            mirrorControllerRight.transform.position = rightController.transform.position + new Vector3(0f, mirrorRealDifference, 0);
            mirrorControllerRight.transform.rotation = rightController.transform.rotation;
        }
    }
    public void CreateMirrorKeyboard(float startingX, float startingY, float startingZ)
    {

        bool empty = ReadKeyPositions();
        if (empty)
            CreateDefaultPoints(startingX, startingY, startingZ);
        else
        {
            CreateCustomPoints(startingX, startingY, startingZ);
            kw.keys = kw.keys.GetRange(32, 32);
        }
        // creating the mirror keyboard on top
        MirrorKeys(startingX, startingY + mirrorRealDifference, startingZ);
        // create mirrored controllers
        SetUpMirrorControllers(startingX, startingY, startingZ);
    }


    public void CreateDefaultPoints(float startingX, float startingY, float startingZ)
    {
        keyboardModelPosition = new Vector3(startingX, startingY, startingZ);
        // delete
        GameObject go = CreateKey(-0.15f + startingX, 0.14f + startingY, 0.05f + startingZ, "DEL");
        Vector3 scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
        go = CreateKey(0.15f + startingX, 0.14f + startingY, 0.05f + startingZ, "DEL");
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
        CreateLine(-0.15f + startingX, 0.06f + startingY, -0.05f + startingZ, -10f, smallerXradius, smallerYradius, "qwert");
        CreateLine(0.15f + startingX, 0.06f + startingY, -0.05f + startingZ, 10f, smallerXradius, smallerYradius, "yuiop");
        CreateLine(-0.15f + startingX, 0f + startingY, startingZ, -10f, xradius, yradius, "asdfg");
        CreateLine(0.15f + startingX, 0f + startingY, startingZ, +10f, xradius, yradius, "hjkl;");
        CreateLine(-0.15f + startingX, -0.06f + startingY, -0.05f + startingZ, -10f, smallerXradius, smallerYradius, "zxcv");
        CreateLine(0.15f + startingX, -0.06f + startingY, -0.05f + startingZ, 10f, smallerXradius, smallerYradius, "bnm,");
        //GameObject del = CreateKey(0.15f, startingY, 0.1f + startingZ, "DEL");

        // space
        go = CreateKey(-0.15f + startingX, -0.18f + startingY, 0.05f + startingZ, "start");
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
        go = CreateKey(0.15f + startingX, -0.18f + startingY, 0.05f + startingZ, " ");
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
    }

    public void CreateCustomPoints(float startingX, float startingY, float startingZ)
    {
        keyboardModelPosition = new Vector3(startingX, startingY, startingZ);
        for (int i = 0; i< KEYS_NUMBER; i++)
        {
            KeyWrapper key = kw.keys[i];
            GameObject go = CreateKey(key.x + startingX, key.y + startingY, key.z + startingZ, key.text);
        }
    }

    void CreateLine(float offsetX, float offsetY, float offsetZ, float angleOffset, float xradius, float yradius, string letters)
    {
        float x;
        float z;

        float angle = 300f - letters.Length * 2f + angleOffset;
        for (int i = 0; i < (letters.Length); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            CreateKey(x + offsetX, offsetY, z + offsetZ, "" + letters[i]);
            angle += (180f / (letters.Length)) % 360;
        }
    }

    GameObject CreateKey(float x, float y, float z, string s, bool mirror = false)
    {
        GameObject go = Instantiate(PF_Key, new Vector3(x, y, z), new Quaternion(0, 0, 0, 0)); ;
        go.transform.SetParent(this.transform);
        XRKey key = go.GetComponent<XRKey>();
        points.Add(go);
        KeyWrapper keywrappper = new KeyWrapper(s, x - keyboardModelPosition.x, y - keyboardModelPosition.y, z - keyboardModelPosition.z);
        key.Setup(s, this, Button_Timer, keywrappper);
        if (mirror == false) // no need to store mirror keys
            kw.keys.Add(keywrappper);
        return go;
    }

    // remove the whole keyboard
    public void DestroyPoints()
    {
        foreach (GameObject point in points) {
            Destroy(point);
        }
        points.Clear();
        kw.keys.Clear();
        DestroyMirrorControllers();
    }


    void MirrorKeys(float startingX, float startingY, float startingZ)
    {
        for (int i = 0; i < KEYS_NUMBER; i++)
        {
            KeyWrapper key = kw.keys[i];
            GameObject go = CreateKey(key.x + startingX, key.y + startingY, key.z + startingZ, key.text, true);
            points[i].GetComponent<XRKey>().AssignMirroredKey(go);
        }
    }

    public void SaveKeyPositions()
    {

        string filename = "positions.JSON";
        string json;
        kw.keyboardName = "lower";
        json = JsonUtility.ToJson(kw);
        StreamWriter writer = new StreamWriter(filename, false);

        try
        {
            writer.Write(json);
        }
        catch (Exception exp)
        {
            print(exp.Message);
        }
        finally
        {
            writer.Close();
        }
    }
    public bool ReadKeyPositions()
    {
        String json;
        try
        {
            json = File.ReadAllText("positions.JSON");
            kw = JsonUtility.FromJson<KeyboardWrapper>(json);
        }
        catch (Exception exp)
        {
            print(exp.Message);
        }

        if (kw == null || kw.keys == null || kw.keys.Count == 0)
        {
            kw = new KeyboardWrapper();
            return true; // positions.JSOn is empty or malformed
        }
        return false; // positions.JSON is not empty
    }

    private void SetUpMirrorControllers(float x, float y, float z)
    {
        mirrorControllerLeft = Instantiate(controllerPF, new Vector3(x, y, z), new Quaternion(0, 0, 0, 0));
        mirrorControllerLeft.name = "mirrorControllerLeft";
        mirrorControllerRight = Instantiate(controllerPF, new Vector3(x, y, z), new Quaternion(0, 0, 0, 0));
        mirrorControllerRight.name = "mirrorControllerRight";
    }

    private void DestroyMirrorControllers()
    {
        Destroy(mirrorControllerLeft);
        Destroy(mirrorControllerRight);
    }
}

