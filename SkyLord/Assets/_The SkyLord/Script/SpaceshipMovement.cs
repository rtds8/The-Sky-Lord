using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activaStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    [SerializeField] private float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    [SerializeField] private float rollSpeed = 90f, rollAcceleration = 3.5f;
    [SerializeField] private CameraManager cameraManager;

    [SerializeField] bool horizontal, vertical, hover, roll;

    private void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        //mouse input
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);


        //roll
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
        roll = !Mathf.Approximately(Input.GetAxis("Roll"), 0.0f);
        if (roll)
            transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        //forward, hover and strafe
        horizontal = !Mathf.Approximately(Input.GetAxis("Horizontal"), 0.0f);
        vertical = !Mathf.Approximately(Input.GetAxis("Vertical"), 0.0f);
        hover = !Mathf.Approximately(Input.GetAxis("Hover"), 0.0f);

        if (horizontal || vertical || hover)
        {
            transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, 0, Space.Self);
            if (cameraManager.FPP.enabled)
                cameraManager.FPP.transform.localRotation = Quaternion.Lerp(Quaternion.identity, cameraManager.FPP.transform.localRotation, 0.8f); 
            if(cameraManager.TPP.enabled)
                cameraManager.TPP.transform.localRotation = Quaternion.Lerp(Quaternion.identity, cameraManager.TPP.transform.localRotation, 0.8f);
            cameraManager.currentRotation = new Vector3(0, 0, 0);
            // FPP.transform.localRotation = new Quaternion(0, 0, 0, 0);
            //FPP.transform.localRotation = fppOrgTrans.localRotation;//Quaternion.Lerp(fppOrgTrans.localRotation, FPP.transform.localRotation, 0.5f);
            // FPP.transform.position = transform.position;
            // FPP.transform.rotation = transform.rotation;
        }

        //look around
        if (Input.GetMouseButton(0))
        {
            if (cameraManager.FPP.enabled)
                cameraManager.FPPCamera();
            if (cameraManager.TPP.enabled)
                cameraManager.TPPCamera();
        }

        //move forward
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activaStrafeSpeed = Mathf.Lerp(activaStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activaStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

    }

}