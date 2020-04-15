﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]

public class VRHeadphone : VREquipment
{
    //public GameObject menuAudio;
    //Controller_GameMenu gameMenu;

    public XRGrabInteractable XRGrabHeadphone;

    public float volumeIncreaseRate = 0.003f;
    public float volumeDecreaseRate = -0.003f;

    public void Start()
    {

    }

    public override void OnActivated(XRBaseInteractor obj)
    {
        print("Activated 2" + this.name);
        Core.Ins.AudioManager.PlayPauseMusic();
    }
    public override void OnDeactivated(XRBaseInteractor obj)
    {
        print("Deactivated 2" + this.name);
    }

    public override void HandleGesture(ENUM_XROS_Gesture gesture)
    {
        switch (gesture)
        {
            case ENUM_XROS_Gesture.up:
                Core.Ins.AudioManager.AdjustVolume(volumeIncreaseRate, Audio_Type.master);
                //Debug.Log("upincrease");
                break;
            case ENUM_XROS_Gesture.down:
                Core.Ins.AudioManager.AdjustVolume(volumeDecreaseRate, Audio_Type.master);
                //Debug.Log("downdecrease");
                break;
            case ENUM_XROS_Gesture.forward:
                break;
            case ENUM_XROS_Gesture.backward:
                break;
            case ENUM_XROS_Gesture.rotate_clockwise:
                break;
            case ENUM_XROS_Gesture.rotate_counterclockwise:
                break;
            default:
                break;
        }
    }
}
