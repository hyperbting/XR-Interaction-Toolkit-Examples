﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Text_ShowDevLog : MonoBehaviour
{
    string compiledMessages = "";
    //List<string> logMessages = new List<string>();
    public TMP_Text text;

    public void Awake()
    {
        if (!text)
        {
            text = this.GetComponent<TMP_Text>();
        }
        Dev.EVENT_NewLog += CompileMessage;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Dev.EventHandler_NewLog += CompileMessage;
        //Dev.Log("hello");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F9))
        {
            Dev.Log("Test");
        }
    }

    public void CompileMessage(string s)
    {
        //logMessages.Add(s);

        compiledMessages = "\n" + s + compiledMessages;
        text.text = compiledMessages;
    }
}