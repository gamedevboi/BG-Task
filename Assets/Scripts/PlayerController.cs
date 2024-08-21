using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR;

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
   
   [SerializeField]
   public Transform hand;

   [SerializeField]
public Transform titleScreen;


   [SerializeField]
   public Animation anim;
   
   public bool isInInventory = false;


   [SerializeField]
   public TMP_Text HealthTxt;
   public int MaxHP = 100;
   public int HP = 50;

   private AudioSource healSound = new();

   
   


   void Awake(){
      characterController = GetComponent<CharacterController>();
      Cursor.lockState = CursorLockMode.Locked;
       HealthTxt.text = "HP: " + HP.ToString();
       healSound = GetComponent<AudioSource>();
      
   }

   void Update() {
      if (titleScreen.gameObject.activeSelf)
      {
         return;
      }
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
         if (isInInventory){return;}
         float mouseX = Input.GetAxis("Mouse X") * LookSensitivityX;
         float mouseY = Input.GetAxis("Mouse Y") * LookSensitivityY;


         vertRot -= mouseY;            
         vertRot = Mathf.Clamp(vertRot, MinYLookAngle, MaxYLookAngle);
         PlayerCamera.localRotation = Quaternion.Euler(vertRot, 0f, 0f);
         transform.Rotate(Vector3.up * mouseX);

      }
      //Attack anim
       if (Input.GetKeyDown(KeyCode.Mouse0) && isInInventory == false)
       {         
         anim.Play("Attack");
         // Animation animation = hand.GetComponent<Animation>();
         // animation.Play();
      }


      // quit to title screen

      if (Input.GetKeyDown(KeyCode.Escape))
        {titleScreen.gameObject.SetActive(true);}

      
   }
   public void Heal(int Amount){
      healSound.Play();
      HP+= Amount;
      if (HP>MaxHP){
         HP =MaxHP;
      }

      HealthTxt.text = "HP: " + HP.ToString();
   }

    public void Equip(InventoryItem inventoryItem)
    {
        Debug.Log("equiped " + inventoryItem.item.name);
        hand.gameObject.SetActive(true);
        
    }

     public void Unequip(InventoryItem inventoryItem)
    {
        Debug.Log("unequiped " + inventoryItem.item.name);
        hand.gameObject.SetActive(false);
        
    }


}
