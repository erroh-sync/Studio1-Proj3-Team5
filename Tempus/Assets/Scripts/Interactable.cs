﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    #region Serialized Variables

    [SerializeField]
    private string NameString = "Interactable Object"; // The name of the object
    [SerializeField]
    private string DialougeString = "This is a dialouge string"; // The text corresponding to the spoken dialouge
    [SerializeField]
    private float InteractDistance = 5.0f; // How far away can we interact with this object
    [SerializeField]
    private Vector2 LabelDimensions = new Vector2(128.0f, 64.0f); // The dimensions of the popup Label
    [SerializeField]
    private float EventDuration = 10.0f; // How long the event will be activate for
    [SerializeField]
    private ReactiveObject[] reactiveObjects; // An array of objects that are activated upon 
    [SerializeField]
    private GUIStyle guiStyle;

    #endregion

    #region Private Variables

    private Vector2 ScreenLocation; // The location of this object on screen
    bool bInRange = false; // Are we in interaction range
    private float EventTimer = 0.0f; // A timer for deactivating the reactive objects

    #endregion

    #region External References

    private AudioSource NarrationSource; // The component which plays the spoken dialouge

    #endregion

    // Use this for initialization
    void Start ()
    {
        NarrationSource = this.gameObject.GetComponent<AudioSource>(); // Retrieve the narration source
    }

    // Update is called once per frame
    void Update()
    {
        #region Distance Checking

        float dist;
        // Calculate our distance from the player
        dist = Vector3.Distance(this.gameObject.transform.position, PlayerController.playerController.gameObject.transform.position);
        // If we are closer to the player than the InteractDistance, set bInRange to true
        bInRange = (dist < InteractDistance);

        #endregion

        // Retrieve the screen location
        ScreenLocation = PlayerController.playerController.camera.WorldToScreenPoint(this.transform.position);

        if (EventTimer > 0.0f)
        {
            EventTimer -= Time.deltaTime;
            if (EventTimer < 0.0f)
                EndEvent();
        }
    }

    // OnGui is called once per frame
    void OnGUI ()
    {
        if (bInRange)
        {
            GUI.Label(new Rect(ScreenLocation.x, Screen.height - ScreenLocation.y, LabelDimensions.x, LabelDimensions.y), NameString, guiStyle);
        }
    }

    // OnUsed is called when the player interacts with us
    public void OnUsed()
    {
        EventTimer = EventDuration; // Start the timer
        NarrationSource.Play(); // Play the narration
        foreach (ReactiveObject obj in reactiveObjects)
            obj.Activate();
    }

    private void EndEvent()
    {
        foreach (ReactiveObject obj in reactiveObjects)
            obj.Deactivate();
    }
}
