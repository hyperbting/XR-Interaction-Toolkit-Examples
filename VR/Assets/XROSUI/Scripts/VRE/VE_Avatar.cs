﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VE_Avatar : VrEquipment
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    protected override void OnHoverEnter(XRBaseInteractor obj)
    {
        Core.Ins.Avatar.ShowAvatarManagementMode(true);
    }

    protected override void OnLastHoverExit(XRBaseInteractor obj)
    {
        Core.Ins.Avatar.ShowAvatarManagementMode(false);
    }
    
    protected override void OnActivate(XRBaseInteractor obj)
    {
        //Toggle On/Off Avatar Management Mode
        Core.Ins.Avatar.ToggleAvatarManagementModeLock();
    }

    public override void HandleGesture(ENUM_XROS_Gesture gesture, float distance)
    {
        switch (gesture)
        {
            case ENUM_XROS_Gesture.Up:
                break;
            case ENUM_XROS_Gesture.Down:
                break;
            case ENUM_XROS_Gesture.Forward:
                break;
            case ENUM_XROS_Gesture.Backward:
                break;
            case ENUM_XROS_Gesture.Left:
                Core.Ins.Avatar.PreviousAlternateAvatar();
                break;
            case ENUM_XROS_Gesture.Right:
                Core.Ins.Avatar.NextAlternateAvatar();
                break;
            case ENUM_XROS_Gesture.RotateClockwise:
                break;
            case ENUM_XROS_Gesture.RotateCounterclockwise:
                break;
            default:
                break;
        }
    }
}
