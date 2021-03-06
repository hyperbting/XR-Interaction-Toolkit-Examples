﻿using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework.Constraints;
using UnityEngine;

//public delegate void Delegate_NewAvatar();

/// <summary>
/// Virtual Equipment System
/// 
/// </summary>
public class Manager_VES : MonoBehaviour
{
    public VES_GestureFeedback VES_GestureFeedback;
    public GameObject PF_DefaultMirrorObject;
    //public static event Delegate_NewAvatar EVENT_NewAvatar;


    public float displayTime = 0.5f;

    private float _timeRemaining;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (_timeRemaining < 0)
        {
            _timeRemaining = 0;
            VES_GestureFeedback.gameObject.SetActive(false);
        }
        else
        {
            _timeRemaining -= Time.deltaTime;
        }
        DebugUpdate();
    }

    private void DebugUpdate()
    {
        
    }

    public void UpdateGestureFeedback(ENUM_XROS_EquipmentGesture equipmentGesture, VE_EquipmentBase veb)
    {
        if (!VES_GestureFeedback.gameObject.activeSelf)
        {
            VES_GestureFeedback.gameObject.SetActive(true);
        }
        
        _timeRemaining = displayTime;
        VES_GestureFeedback.UpdateGestureFeedback(equipmentGesture, veb);
    }

}