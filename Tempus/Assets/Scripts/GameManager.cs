using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    #region Serialized Variables

    //[SerializeField]

    #endregion

    #region Private Variables

    private int totalNumInteractables = 0; // The rotation around the Y/Horizontal axis
    private int numInteractablesInteracted = 0;

    #endregion

    #region External References

    private ElevatorController IntroElevator; // The elevator we will enter from
    private ElevatorController OutroElevator; // The elevator we will exit from

    #endregion

    // Use this for initialization
    void Start () {
        totalNumInteractables = GameObject.FindGameObjectsWithTag("Interactable").Length;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
