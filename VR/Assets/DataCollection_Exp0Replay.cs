﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = System.Random;

public class DataCollection_Exp0Replay : MonoBehaviour
{
    public string filePath = "Assets/XROSUI/ML_Model/Data/";
    public string fileName = "Exp0_ 2021-02-19-02-12-11 - Duplicates Removed";

    public GameObject ReplayHeadset;
    public GameObject ReplayHandR;
    public GameObject ReplayHandL;
    public GameObject ReplayTracker;

    private List<DataContainer_Exp0> dataList = new List<DataContainer_Exp0>();
    private List<string> stringList;

    // Start is called before the first frame update
    void Start()
    {
        ReadTextFile();
    }

    private void ReadTextFile()
    {
        var inp_stm = new StreamReader(filePath + fileName);

        while (!inp_stm.EndOfStream)
        {
            var inp_ln = inp_stm.ReadLine();

            stringList.Add(inp_ln);
        }

        inp_stm.Close();

        parseList();
    }

    void parseList()
    {
        List<string[]> parsedList = new List<string[]>();
        for (int i = 1; i < stringList.Count; i++)
        {
            string[] temp = stringList[i].Split(',');
            for (int j = 0; j < temp.Length; j++)
            {
                temp[j] = temp[j].Trim(); //removed the blank spaces
            }

            parsedList.Add(temp);
        }
        //you should now have a list of arrays, ewach array can ba appied to the script that's on the Sprite
        //you'll have to figure out a way to push the data the sprite

        for (int i = 0; i < parsedList.Count; i++)
        {
            DataContainer_Exp0 d = new DataContainer_Exp0();
            d.StringToData(parsedList[i]);
            dataList.Add(d);
        }
    }

    public bool startPlayback = false;

    private float startTime = 0;

    private int currentIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            startPlayback = true;
            startTime = Time.time;
        }

        if (startPlayback)
        {
            //if (Time.time > startTime + dataList[currentIndex].timestamp)
            {
                ModifyPosition();
            }
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            ModifyPosition();
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            RandomPosition();
        }
    }

    void ModifyPosition()
    {
        if (currentIndex < dataList.Count)
        {
            //print("currentIndex: " + currentIndex + " at " + Time.time);
            ReplayHeadset.transform.localPosition = dataList[currentIndex].headPos;
            ReplayHeadset.transform.localRotation = dataList[currentIndex].headRotQ;
            ReplayHandR.transform.localPosition = dataList[currentIndex].HandRPos;
            ReplayHandR.transform.localRotation = dataList[currentIndex].handRRotQ;
            ReplayHandL.transform.localPosition = dataList[currentIndex].handLPos;
            ReplayHandL.transform.localRotation = dataList[currentIndex].handLRotQ;
            ReplayTracker.transform.localPosition = dataList[currentIndex].tracker1Pos;
            ReplayTracker.transform.localRotation = dataList[currentIndex].tracker1RotQ;

            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
    }

    public void RandomPosition()
    {
        currentIndex = (int) UnityEngine.Random.Range(0, dataList.Count);
        //Debug.Log(currentIndex);
    }
}