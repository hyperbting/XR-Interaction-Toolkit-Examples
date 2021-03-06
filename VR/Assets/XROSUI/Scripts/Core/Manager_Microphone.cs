﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Reference: https://www.youtube.com/watch?v=GHc9RF258VA&list=PLJqYhYXx9Wpe43s7_36andLevASAILGyy&index=2
/// </summary>
public class Manager_Microphone : MonoBehaviour
{
    //Assign this audiosource from hierarchy to use for debug. 
    //This should be part of the prefab containing this script
    public AudioSource assignedDebugAudioSource;

    //this tracks whether there are any connected microphones
    private bool _hasMicrophone = false;

    //This tracks all the available recording devices
    string[] _recordingDevices;

    int _currentSelectedDeviceId;
    private string _selectedDevice;

    void Start()
    {
        this.LoadDevices();
    }

    // Start is called before the first frame update
    public void LoadDevices()
    {
        if (Microphone.devices.Length > 0)
        {
            _hasMicrophone = true;
            _recordingDevices = Microphone.devices;
            
            for(int i=0; i<_recordingDevices.Length; i++)
            {
//                print(i + ": " + _recordingDevices[i].ToString());
            }
            
            _selectedDevice = _recordingDevices[0].ToString();
        }
        else
        {
            _hasMicrophone = false;
        }
//        print("selected device: " + _selectedDevice);
//        print("_hasMicrophone " + _hasMicrophone);

    }

    public void SetDevice(int i)
    {
        if (i < 0)
        {
            i = 0;
        }
        else if (i > _recordingDevices.Length)
        {
            i = _recordingDevices.Length - 1;
        }

        _currentSelectedDeviceId = i;
    }

    private void Debug_StartRecording()
    {
        if (_hasMicrophone)
        {
            print("[Debug] Start recording");
            assignedDebugAudioSource.clip = GetAudioClipFromSelectedMicrophone();
            assignedDebugAudioSource.Play();
        }
    }

    private void Debug_StopRecording()
    {
        if (_hasMicrophone)
        {
            print("[Debug] Stop recording");
            assignedDebugAudioSource.Stop();
        }
    }


    /// <summary>
    /// Use this
    /// </summary>
    /// <returns></returns>
    public AudioClip GetAudioClipFromSelectedMicrophone()
    {
        //AudioSettings.outputSampleRate by default is 44k
        //Microphone.Start(_selectedDevice, true, 5, 44100);
        return Microphone.Start(_selectedDevice, true, 5, AudioSettings.outputSampleRate);
    }

    public void SelectNewDevice(string s)
    {
        _selectedDevice = s;
    }

    // Update is called once per frame
    void Update()
    {
        //DebugUpdate();
    }

    void DebugUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            print("O key");
            Debug_StartRecording();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            print("P key");
            assignedDebugAudioSource.Play();
        }
    }
}