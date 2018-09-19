using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{    public float PlayerCameraDistance { get; set; }
    public Transform cameraTarget;

    Camera playerCamera;
    float zoomSpeed = 35f;

    void Start()
    {
        PlayerCameraDistance = 25f;
        playerCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            playerCamera.fieldOfView -= scroll * zoomSpeed;
            playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView, 40, 80);
        }

        transform.position = new Vector3(cameraTarget.position.x, cameraTarget.position.y + PlayerCameraDistance, cameraTarget.position.z - PlayerCameraDistance);
    }

//Note: Alternate CameraControleler
    /*
    public Transform target;

    public Vector3 offset;
    public float zoomSpeed = 7f;
    public float minZoom = 7f;
    public float maxZoom = 20f;
    public float pitch = 2f;
    public float yawSpeed = 100f;

    private float currentZoom = 10f;
    private float currentYaw = 0f;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
	}

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }*/
}
