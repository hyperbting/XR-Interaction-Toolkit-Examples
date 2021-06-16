﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GestureArea : MonoBehaviour
{
    //PY: This is really just the center of the gesture area/equipment slot. It should not move so we can see how the equipped equipment moved away from it. 
    //This GestureArea/Socket Should be refactored
    public GameObject GestureCore;
    public GameObject GO_VE;
    private VE_EquipmentBase VE;
    //public float volume;
    [Tooltip("How long before an equipment gesture can be triggered again")]
    public float coolDown = 0.5f;
    //Not working atm
    [FormerlySerializedAs("legalGestureDistance")]
    [Tooltip("The maximum distance before the gesture is not considered a legal equipment gesture")]
    public float maxLegalGestureDistance = 0.5f;
    //Not used yet
    [Tooltip("The minimum distance before the gesture is not considered a legal equipment gesture")]
    public float minLegalGestureDistance = 0.05f;
    private float _lastAskTime = 0;
    [SerializeField]
    private float gestureDistance;


    // Start is called before the first frame update
    private void Start()
    {
        GestureCore = this.gameObject;

        if (GO_VE)
        {
            this.RegisterVREquipment(GO_VE.GetComponent<VE_EquipmentBase>());
        }
    }

    public void RegisterVREquipment(VE_EquipmentBase vre)
    {
        this.VE = vre;
        this.GO_VE = vre.gameObject;
    }

    public void UnregisterVREquipment()
    {
        this.VE = null;
        this.GO_VE = null;
    }

    // Update is called once per frame
    private void Update()
    {
        //Virtual Equipment exists and is grabbed by player
        if (VE && VE.IsSelected())
        {
            if (WithinLegalGestureDistance())
            {
                if (_lastAskTime + coolDown < Time.time)
                {
                    MeasureDirect();
                    _lastAskTime = Time.time;
                }
            }
            
            
            
        }

        DebugUpdate();
    }

    private bool WithinLegalGestureDistance()
    {
        //Measure distance between the equipment slot and the moved position
        gestureDistance = Vector3.Distance(GestureCore.transform.position, GO_VE.transform.position);
        return gestureDistance <= maxLegalGestureDistance;
    }

    private void DebugUpdate()
    {
        /*
        if (Input.GetKey(KeyCode.Alpha7))
        {
            Dev.Log("[Debug] Register Equipment");
            GameObject go = GameObject.Find("Headphone2");
            this.RegisterVREquipment(go.GetComponent<VREquipment>());
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            Dev.Log("[Debug] Deregister Equipment");
            this.UnregisterVREquipment();
        }
        if (Input.GetKey(KeyCode.O))
        {
            Dev.Log("[Debug] Move Equipment Up");
            GO_VE.transform.position += Vector3.up;
        }
        if (Input.GetKey(KeyCode.L))
        {
            Dev.Log("[Debug] Move Equipment Down");
            Dev.Log(GO_VE.transform.position);
            GO_VE.transform.position += Vector3.down;
            Dev.Log(GO_VE.transform.position);
        }
        if (GO_VE)
        {
            if (Input.GetKey(KeyCode.Alpha9))
            {
                Dev.Log("test");
                MeasureDirect();
            }
        }
        */
    }

    private void MeasureDirect()
    {
        var obj2Position = GO_VE.transform.position;
        var obj1Position = GestureCore.transform.position;
        var differenceVector = obj2Position - obj1Position;
        var angle = Vector3.Angle(GestureCore.transform.forward, differenceVector);
        
        bool facingForward = (Mathf.Abs(angle) < 30);
        
        if (Mathf.Abs(differenceVector.y) >= Mathf.Abs(differenceVector.z))
        {
            if (differenceVector.y > 0)
            {
                this.VE.HandleGesture(ENUM_XROS_EquipmentGesture.Up, differenceVector.y);
            }
            else if (differenceVector.y < 0)
            {
                this.VE.HandleGesture(ENUM_XROS_EquipmentGesture.Down, differenceVector.y);
            }
        }
        else
        {
            if ((differenceVector.z > 0 && facingForward) || (differenceVector.z < 0 && !facingForward))
            {
                this.VE.HandleGesture(ENUM_XROS_EquipmentGesture.Right, differenceVector.z);
            }
            else if ((differenceVector.z < 0 && facingForward) || (differenceVector.z > 0 && !facingForward))
            {
                this.VE.HandleGesture(ENUM_XROS_EquipmentGesture.Left, differenceVector.z);
            }
        }
    }
}