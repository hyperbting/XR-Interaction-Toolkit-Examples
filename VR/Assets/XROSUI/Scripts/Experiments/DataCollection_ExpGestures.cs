﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum Gesture {HeadUp, HeadDown, HeadForward, HeadBackward}

public class DataCollection_ExpGestures : DataCollection_ExpBase, IWriteToFile
{
    // To be set in Unity to determine which gesture we are currently collecting data for
    public Gesture gesture;
    public XRGrabInteractable grabInteractable;
    
    // How often to sample the position for a gesture/movement
    private static double _timestepSec = 0.1;
    // How many position samples to include in a gesture/movement
    public static int samplesPerGesture = 10;

    // Running queue of last _samples_per_gesture positions
    private Queue<DataContainer_ExpGesturesPosition> _lastPositions = new Queue<DataContainer_ExpGesturesPosition>();
    // Lock to make sure the lastPositions queue is safe to edit or read
    private readonly object _lastPositionsLock = new object();
    
    // List of entire data for the experiment
    private List<DataContainer_ExpGestures> _dataList = new List<DataContainer_ExpGestures>();
    
    private GameObject _head;
    private GameObject _handR;
    private GameObject _handL;

    private float _lastUpdateTime;

    private bool startedGesture = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ExpName = "ExpGestures";
        grabInteractable.onSelectEnter.AddListener(StartGesture);
        grabInteractable.onSelectExit.AddListener(EndGesture);
        ReloadXrDevices();
    }

    private void OnDisable()
    {
        grabInteractable.onSelectEnter.RemoveListener(StartGesture);
        grabInteractable.onSelectExit.RemoveListener(EndGesture);
    }

    private void StartGesture(XRBaseInteractor arg0)
    {
        startedGesture = true;
    }

    private void ReloadXrDevices()
    {
        Dev.Log("Reload Xr Devices");
        _head = Core.Ins.XRManager.GetXrCamera().gameObject;
        _handR = Core.Ins.XRManager.GetRightDirectControllerGO();
        _handL = Core.Ins.XRManager.GetLeftDirectController();
    }

    public override void StartRecording()
    {
        base.StartRecording();
        ReloadXrDevices();
    }

    public override void StopRecording()
    {
        base.StopRecording();
    }

    public override void LateUpdate()
    {
        lock (_lastPositionsLock)
        {
            if (_lastPositions.Count == 0 || Time.time - _lastUpdateTime >= _timestepSec)
            {
                var data = new DataContainer_ExpGesturesPosition
                {
                    headPos = _head.transform.localPosition,
                    headRot = _head.transform.eulerAngles,
                    headRotQ = _head.transform.rotation,
                    handRPos = _handR.transform.localPosition,
                    handRRot = _handR.transform.eulerAngles,
                    handRRotQ = _handR.transform.rotation,
                    handLPos = _handL.transform.localPosition,
                    handLRot = _handL.transform.eulerAngles,
                    handLRotQ = _handL.transform.rotation
                };
                _lastPositions.Enqueue(data);
                _lastUpdateTime = Time.time;
                if (_lastPositions.Count > samplesPerGesture)
                {
                    _lastPositions.Dequeue();
                }
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
    }

    public override string OutputFileName()
    {
        return ExpName + gesture + "_ " + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".csv";
    }

    public override string OutputData()
    {
        print("Outputting data");
        var sb = new StringBuilder();
        sb.Append(DataContainer_ExpGestures.HeaderToString());
        foreach (var d in _dataList)
        {
            sb.Append(d);
        }
        return sb.ToString();
    }

    public void EndGesture(XRBaseInteractor xrBaseInteractor)
    {
        if (!startedGesture)
            return;

        //if (Core.Ins.XRManager.GetRightRayController().)
        {
            
        }
        
        print("Trying to add to data list");
        // Make sure we have control of the lastPositions queue
        lock (_lastPositionsLock)
        {
            // Make sure there are the proper number of position samples to make a gesture
            if (_lastPositions.Count != samplesPerGesture)
                return;

            var data = new DataContainer_ExpGestures
            {
                positions = new List<DataContainer_ExpGesturesPosition>(_lastPositions),
                gesture = gesture
            };
            
            _dataList.Add(data);
            Dev.Log("Added a gesture of " + gesture + " to data list");
        }

        startedGesture = false;
    }

    public void ChangeExperimentType(Gesture gesturesToRecord)
    {
        Dev.Log("Collecting gesture of type " + gesturesToRecord);
        this.gesture = gesturesToRecord;
    }

    public void RemoveLastGesture()
    {
        if (_dataList.Any())
        {
            Dev.Log("Removed Last Gesture");
            _dataList.RemoveAt(_dataList.Count - 1);
        }
        else
        {
            Dev.Log("No Gesture to remove");
        }
    }

    public int GetTotalEntries()
    {
        return _dataList.Count;
    }
}
