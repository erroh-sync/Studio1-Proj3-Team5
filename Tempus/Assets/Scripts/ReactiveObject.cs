using UnityEngine;
using System.Collections;

public class ReactiveObject : MonoBehaviour {

    #region Serialized Variables

    [SerializeField]
    private Vector3 forceVector = new Vector3(0.0f, 0.0f, 0.0f); // The name of the object

    #endregion


    #region External References

    private Rigidbody rb; // The component which plays the spoken dialouge

    #endregion

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void Activate()
    {
        rb.useGravity = true;
        rb.AddForce(forceVector);
    }

    public void Deactivate()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f); // Reset the velocity to zero
    }
}
