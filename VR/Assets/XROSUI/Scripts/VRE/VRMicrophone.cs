﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMicrophone : VREquipment
{
    public override void OnActivated(XRBaseInteractor obj)
    {
        Core.Ins.RecordManager.StartRecording();
    }
    public override void OnDeactivated(XRBaseInteractor obj)
    {
        Core.Ins.RecordManager.StopRecording();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}