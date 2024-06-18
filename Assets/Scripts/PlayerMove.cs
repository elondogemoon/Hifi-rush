using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _MoveSpeed = 5.0f;
    [SerializeField] private float _RotateSpeed = 1.0f;
    [SerializeField] private Animator _Animator;
    Vector3 moveDirection;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float turnSpeed = 1.0f;
    
    private CharacterController _CharacterController;

    private void Awake()
    {
        _CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Vertical");
        float z = Input.GetAxisRaw("Horizontal");
        float r = Input.GetAxis("Mouse X");

        //moveDirection = new Vector3(-x, 0, z).normalized;

        // 입력값에 따라 캐릭터를 회전시킵니다.
        if (x != 0 || z != 0)
        {
            Vector3 cameraForward = virtualCamera.transform.forward;
            cameraForward.y = 0f; // y 축은 회전하지 않도록 설정합니다.

            Vector3 moveDirection = -z * cameraForward + x * virtualCamera.transform.right;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
                transform.rotation = targetRotation;
            }
            _CharacterController.Move(-transform.right * _MoveSpeed * Time.deltaTime);
            
        }
        float moveMagnitude = new Vector2(x, z).magnitude;
        _Animator.SetFloat("MoveSpeed", moveMagnitude);

        
    }
}

