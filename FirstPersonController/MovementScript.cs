using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Ссылка на Character Controller
    private CharacterController Character;

    // Скорость при ходьбе
    public float walkSpeed = 20f;

    // Скорость при беге
    public float runSpeed = 40f;

    // Скорость при подкрадывании
    public float crouchSpeed = 10f;

    // Параметр скорости передвижения
    private float speed;

    // Значение гравитационной постоянной g
    private float gravity = -2f;

    // Значение при изменении скорости падения
    private Vector3 velocity;

    private Transform GroundCheck;

    // Расстояние до земли, на которое будет триггериться объект
    private float groundDistance = 0.4f;

    // Маска для распознавания земли
    public LayerMask groundMask;

    // Изменяется на true после приземления СС на ground
    bool isGrounded = false;

    // Изменяется на true на время подкрадывания
    private bool isCrouch = false;

    // Переменная силы прыжка
    public float jumpForce = 1000;

    // Start is called before the first frame update
    void Start()
    {
        Character = GetComponent<CharacterController>();
        GroundCheck = Character.transform.Find("GroundCheck").transform;
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Установка распознавания ввода с клавиш WASD
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Логика изменения координат
            Vector3 move = transform.right * x + transform.forward * z;

            // Вызов метода Move() на СС с формулой для пердвижения
            Character.Move(move * speed * Time.deltaTime);

            // Изменение координаты Y в векторе velocity
            velocity.y += gravity + Time.deltaTime;

            // Вызов Move() для движения в падении из логик и формулы
            // из логики формулы ускорения свободного падения h = (gt^2)/2
            Character.Move((velocity * Time.deltaTime) / 2);

            // Переменная становится истинной, если невидимая сфера, созданная 
            // в координатах GroundCheck.position с радиусом groundDistance 
            // соприкасается cо слоем groundMask
            isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

            // Проверка истинности выражения
            // (значение -2f берется только из-за лучшего эффекта, 0f также допустимо)
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // При нажатии клавиши Shift СС бегает
            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
            {
                speed = runSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
            {
                speed = walkSpeed;
            }

            // При нажатии клавиши Control СС кродёться
            if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
            {
                isCrouch = true;
                speed = crouchSpeed;
                Character.height = 1f; // Уменьшение высоты CC 
            }
            if (Input.GetKeyUp(KeyCode.LeftControl)/* && isGrounded*/)
            {
                isCrouch = false;
                speed = walkSpeed;
                Character.height = 2f;
            }

            // при нажатии клавиши Space (по умолчанию) СС подпрыгивает
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouch)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }
    }
}
