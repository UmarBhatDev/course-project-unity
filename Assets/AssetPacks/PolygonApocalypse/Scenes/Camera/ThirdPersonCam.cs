using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject _tppCamera;
    public GameObject _aimCamera;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Aim,
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1)) SwitchCameraStyle(CameraStyle.Aim);
        if (Input.GetMouseButtonUp(1)) SwitchCameraStyle(CameraStyle.Basic);

        var viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if(currentStyle == CameraStyle.Basic)
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        else if(currentStyle == CameraStyle.Aim)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        _tppCamera.SetActive(false);
        _aimCamera.SetActive(false);

        if (newStyle == CameraStyle.Basic) _tppCamera.SetActive(true);
        if (newStyle == CameraStyle.Aim) _aimCamera.SetActive(true);

        currentStyle = newStyle;
    }
}
