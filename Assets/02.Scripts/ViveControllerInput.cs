using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveControllerInput : MonoBehaviour
{
    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Input_Sources any = SteamVR_Input_Sources.Any;

    //Action InteractUI
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    //Action Tracked Click
    private SteamVR_Action_Boolean trackPadClick = SteamVR_Actions.default_Teleport;


    //Trackpad Touch
    private SteamVR_Action_Boolean trackPadTouch = SteamVR_Actions.default_trackpadTouch;

    //Trackpad Touch Position(vector2)
    private SteamVR_Action_Vector2 trackPadPosition = SteamVR_Actions.default_trackpadPosition;

    //Grap
    //private SteamVR_Action_Boolean grip = SteamVR_Actions.default_GrabGrip;
    private SteamVR_Action_Boolean grip = SteamVR_Input.GetBooleanAction("GrabGrip");

    //Haptic
    private SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;   

    // HeadSet Sensor
    private SteamVR_Action_Boolean headSet = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("HeadsetOnHead", true);
    


    // Update is called once per frame
    void Update()
    {
        if(trigger.GetStateDown(any))
        {
            Debug.Log("Trigger button Down!");
        }

        if(trackPadClick.GetStateUp(any))
        {
            Debug.Log("Track pad released");
        }

        if(trackPadTouch.GetState(any))
        {
            Vector2 pos = trackPadPosition.GetAxis(any);

            Debug.Log($"pos x = {pos.x}, y = {pos.y}");
        }

        if(grip.GetStateDown(rightHand))
        {
            haptic.Execute(0.2f, 0.5f, 200, 0.5f, rightHand);
        }

        if(headSet.GetStateDown(SteamVR_Input_Sources.Head))
        {
            Debug.Log("Headset on");
        }

        if(headSet.GetStateUp(SteamVR_Input_Sources.Head))
        {
            Debug.Log("Headset off");
        }

    }
}
