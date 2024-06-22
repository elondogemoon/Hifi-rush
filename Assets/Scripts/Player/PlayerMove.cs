using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _MoveSpeed = 5.0f;
    [SerializeField] private float _RotateSpeed = 1.0f;
    [SerializeField] private Animator _Animator;
    [SerializeField] private Camera virtualCamera;
    [SerializeField] private float turnSpeed = 5.0f;

    [SerializeField] public CharacterController _CharacterController;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private float gravity = -9.81f;

    private void Awake()
    {
        _CharacterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // CharacterController�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
        if (_CharacterController == null || !_CharacterController.enabled)
        {
            return; // CharacterController�� ��Ȱ��ȭ�� ��� �̵� ������ �������� ����
        }

        float x = Input.GetAxisRaw("Vertical");
        float z = Input.GetAxisRaw("Horizontal");
        float r = Input.GetAxis("Mouse X");

        if (x != 0 || z != 0)
        {
            Vector3 cameraForward = virtualCamera.transform.forward;
            Vector3 cameraRight = virtualCamera.transform.right;

            // ī�޶��� ���� ���Ϳ� ������ ���͸� ����鿡 ���� (y = 0)
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            // �̵� ���� ���
            moveDirection = z * cameraRight + x * cameraForward;

            // �̵� �������� �÷��̾� ȸ��
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            }

            // ĳ���� �̵�
            _CharacterController.Move(moveDirection * _MoveSpeed * Time.deltaTime);
        }

        // �߷� ����
        if (!_CharacterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }
        _CharacterController.Move(velocity * Time.deltaTime);

        // �ִϸ����� ������Ʈ
        float moveMagnitude = new Vector2(x, z).magnitude;
        _Animator.SetFloat("MoveSpeed", moveMagnitude);
    }
}
