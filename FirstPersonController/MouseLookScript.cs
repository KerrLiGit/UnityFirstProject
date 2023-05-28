using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLookScript : MonoBehaviour
{
    // ����������� ����������������
    public float mouseSensitivity = 200f;

    // ������ FPS
    public Transform Controller;

    // ������� �������� ��������
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // ���������� �������
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // ���� �� ��� X � ��� Y
            float mouseX = Input.GetAxis("Mouse X")
                * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y")
                * mouseSensitivity * Time.deltaTime;

            // ����������� ���� ������ �� 90 �������� ������ � �����
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            // ������� ������ �� ��� X
            Controller.Rotate(Vector3.up * mouseX);
        }
    }

    public void SetMouseSensivity(Slider slider)
    {
        mouseSensitivity = slider.value;
    }
}
