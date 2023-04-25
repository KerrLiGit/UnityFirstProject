using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // ������ �� Character Controller
    private CharacterController Character;

    // �������� ��� ������
    public float walkSpeed = 20f;

    // �������� ��� ����
    public float runSpeed = 40f;

    // �������� ��� �������������
    public float crouchSpeed = 10f;

    // �������� �������� ������������
    private float speed;

    // �������� �������������� ���������� g
    private float gravity = -2f;

    // �������� ��� ��������� �������� �������
    private Vector3 velocity;

    private Transform GroundCheck;

    // ���������� �� �����, �� ������� ����� ������������ ������
    private float groundDistance = 0.4f;

    // ����� ��� ������������� �����
    public LayerMask groundMask;

    // ���������� �� true ����� ����������� �� �� ground
    bool isGrounded = false;

    // ���������� �� true �� ����� �������������
    private bool isCrouch = false;

    // ���������� ���� ������
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
            // ��������� ������������� ����� � ������ WASD
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // ������ ��������� ���������
            Vector3 move = transform.right * x + transform.forward * z;

            // ����� ������ Move() �� �� � �������� ��� �����������
            Character.Move(move * speed * Time.deltaTime);

            // ��������� ���������� Y � ������� velocity
            velocity.y += gravity + Time.deltaTime;

            // ����� Move() ��� �������� � ������� �� ����� � �������
            // �� ������ ������� ��������� ���������� ������� h = (gt^2)/2
            Character.Move((velocity * Time.deltaTime) / 2);

            // ���������� ���������� ��������, ���� ��������� �����, ��������� 
            // � ����������� GroundCheck.position � �������� groundDistance 
            // ������������� c� ����� groundMask
            isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

            // �������� ���������� ���������
            // (�������� -2f ������� ������ ��-�� ������� �������, 0f ����� ���������)
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // ��� ������� ������� Shift �� ������
            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
            {
                speed = runSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
            {
                speed = walkSpeed;
            }

            // ��� ������� ������� Control �� ��������
            if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
            {
                isCrouch = true;
                speed = crouchSpeed;
                Character.height = 1f; // ���������� ������ CC 
            }
            if (Input.GetKeyUp(KeyCode.LeftControl)/* && isGrounded*/)
            {
                isCrouch = false;
                speed = walkSpeed;
                Character.height = 2f;
            }

            // ��� ������� ������� Space (�� ���������) �� ������������
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouch)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }
    }
}
