using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
   public float WalkSpeed = 4f;
   public float SpringMult = 2f;
   public float JumpForce = 5f;
   public float GroundCheckDist = 1.5f;
   public float LookSensitivityX = 1f;
   public float LookSensitivityY = 1f;
   public float MinYLookAngle = -90;
   public float MaxYLookAngle = 90;
   public Transform PlayerCamera;


   private CharacterController characterController;
   private float vertRot = 0f;
 

   void Awake(){
      characterController = GetComponent<CharacterController>();
      // Cursor.lockState = CursorLockMode.Locked;
   }

   void Update() {

      // Movement
      float horizontalMovement = Input.GetAxis("Horizontal");
      float verticalMovement = Input.GetAxis("Vertical");

      Vector3 moveDir = transform.forward * verticalMovement + transform.right * horizontalMovement;
      moveDir.Normalize();

      float speed = WalkSpeed;

      if(Input.GetAxis("Sprint") > 0){
         speed *= SpringMult;  
      }
      characterController.Move(moveDir * speed * Time.deltaTime);


      // Cam Movement

      if (PlayerCamera != null){
         float mouseX = Input.GetAxis("Mouse X") * LookSensitivityX;
         float mouseY = Input.GetAxis("Mouse Y") * LookSensitivityY;


         vertRot -= mouseY;            
         vertRot = Mathf.Clamp(vertRot, MinYLookAngle, MaxYLookAngle);
         PlayerCamera.localRotation = Quaternion.Euler(vertRot, 0f, 0f);
         transform.Rotate(Vector3.up * mouseX);

      }
   }

}
