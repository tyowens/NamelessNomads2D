using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Private Fields
    [Tooltip("The Smoothing speed for the camera to follow the target")]
    [SerializeField]
    private float smoothSpeed = 0.125f;

    private Transform cameraTransform;
    #endregion


    #region MonoBehavior Methods
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Follow();
    }
    #endregion


    #region Private Methods
    // Move Camera to follow local Player Prefab smoothly
    void Follow()
    {
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            // Set camera's destination vector to be the location above the player while maintaining the camera's current z-coord
            Vector3 cameraDestination = this.transform.position;
            cameraDestination.z = cameraTransform.position.z;
            cameraTransform.position = cameraDestination;
        }
    }
    #endregion
}
