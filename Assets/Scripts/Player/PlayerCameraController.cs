using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity = 1f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraPosition;

    private float xRotation;
    private float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSensitivity;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.position = cameraPosition.position;
    }
}
