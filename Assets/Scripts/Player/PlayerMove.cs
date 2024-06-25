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
        // CharacterController가 활성화되어 있는지 확인
        if (_CharacterController == null || !_CharacterController.enabled)
        {
            return; // CharacterController가 비활성화된 경우 이동 로직을 실행하지 않음
        }

        float x = Input.GetAxisRaw("Vertical");
        float z = Input.GetAxisRaw("Horizontal");
        float r = Input.GetAxis("Mouse X");

        if (x != 0 || z != 0)
        {
            Vector3 cameraForward = virtualCamera.transform.forward;
            Vector3 cameraRight = virtualCamera.transform.right;

            // 카메라의 앞쪽 벡터와 오른쪽 벡터를 수평면에 투영 (y = 0)
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            // 이동 방향 계산
            moveDirection = z * cameraRight + x * cameraForward;

            // 이동 방향으로 플레이어 회전
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            }

            // 캐릭터 이동
            _CharacterController.Move(moveDirection * _MoveSpeed * Time.deltaTime);
        }

        // 중력 적용
        if (!_CharacterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }
        _CharacterController.Move(velocity * Time.deltaTime);

        // 애니메이터 업데이트
        float moveMagnitude = new Vector2(x, z).magnitude;
        _Animator.SetFloat("MoveSpeed", moveMagnitude);
    }
}
