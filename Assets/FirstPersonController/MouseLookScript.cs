using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLookScript : MonoBehaviour
{
    // Коэффициент чувствительности
    public float mouseSensitivity = 200f;

    // Объект FPS
    public Transform Controller;

    // Текущее значение поворота
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Блокировка курсора
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Ввод по оси X и оси Y
            float mouseX = Input.GetAxis("Mouse X")
                * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y")
                * mouseSensitivity * Time.deltaTime;

            // Ограничение угла обзора по 90 градусов сверху и снизу
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            // Поворот камеры по оси X
            Controller.Rotate(Vector3.up * mouseX);
        }
    }

    public void SetMouseSensivity(Slider slider)
    {
        mouseSensitivity = slider.value;
    }
}
