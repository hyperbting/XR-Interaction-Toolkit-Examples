﻿using UnityEngine;
using UnityEngine.UI; //create public inputfield 
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SeparateKeyboardCharacterCreator: KeyboardController
{
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

    private void Awake()
    {
        //CreatePoints();

    }

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            inputField.text = "";
        }


    }

    
    public void CreatePoints(float startingX, float startingY, float startingZ)
    {
        print("starting" + startingX + " " + startingY + " " + startingZ);
        // delete
        GameObject go = CreateKey(startingX, startingY, startingZ, "DEL");
        Vector3 scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;

        go = CreateKey(0.15f + startingX, 0.14f + startingY, 0.05f + startingZ, "DEL"); 
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;

        CreateLine(-0.15f + startingX, 0.06f +startingY, -0.05f+startingZ, -10f, smallerXradius, smallerYradius, "qwert");
        CreateLine(0.15f + startingX, 0.06f + startingY, -0.05f + startingZ, 10f, smallerXradius, smallerYradius,"yuiop");
        CreateLine(-0.15f + startingX, 0f+ startingY, startingZ, -10f, xradius, yradius,"asdfg");
        CreateLine(0.15f + startingX, 0f + startingY, startingZ, +10f, xradius, yradius,"hjkl;");
        CreateLine(-0.15f + startingX, -0.06f + startingY, -0.05f + startingZ, -10f, smallerXradius, smallerYradius,"zxcv");
        CreateLine(0.15f + startingX, -0.06f + startingY, -0.05f + startingZ, 10f, smallerXradius, smallerYradius, "bnm,");
        //GameObject del = CreateKey(0.15f, startingY, 0.1f + startingZ, "DEL");

        // space
        go = CreateKey(-0.15f + startingX, -0.18f+startingY, 0.05f+startingZ, "start");
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;

        go = CreateKey(0.15f + startingX, -0.18f+startingY, 0.05f + startingZ, " ");
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
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

    GameObject CreateKey(float x, float y, float z, string s)
    {
        GameObject go = Instantiate(PF_Key, new Vector3(x, y, z), new Quaternion(0, 0, 0, 0)); ;
        go.transform.SetParent(this.transform);
        XRKey key = go.GetComponent<XRKey>();
        key.Setup(s, this, Button_Timer);

        return go;
    }


}

