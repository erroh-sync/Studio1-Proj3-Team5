﻿using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {

    #region Serialized Variables

    [SerializeField]
    private Animation leftDoorAnimation;
    [SerializeField]
    private Animation rightDoorAnimation;

    #endregion

    #region Private Variables

    private float rotY = 0.0f; // The rotation around the Y/Horizontal axis
    private float rotX = 0.0f; // The rotation around the X/Horizontal axis 
    private bool hasJumped = true; // Stores whether or not we have jumped
    private bool mouseLocked = true; // Is the mouse currently locked to the screen?

    #endregion

    // Use this for initialization
    void Start () {
        OpenDoors();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenDoors()
    {
        leftDoorAnimation.Play();
        foreach (AnimationState state in leftDoorAnimation)
        {
            state.speed = 1.0F;
        }

        rightDoorAnimation.Play();
        foreach (AnimationState state in rightDoorAnimation)
        {
            state.speed = 1.0F;
        }
    }

    public void CloseDoors()
    {
        leftDoorAnimation.Play();
        foreach (AnimationState state in leftDoorAnimation)
        {
            state.speed = -1.0F;
        }

        rightDoorAnimation.Play();
        foreach (AnimationState state in rightDoorAnimation)
        {
            state.speed = -1.0F;
        }
    }
}
