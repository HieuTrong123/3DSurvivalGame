﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool PlayerInRange;
    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0) && PlayerInRange && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject==gameObject){
            // Debug.Log("item added to inventory");
            if(!InventorySystem.Instance.CheckIfFull()){
                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }
            else{
                Debug.Log("inventory is full");
            }
          
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            PlayerInRange = false;
        }
    }

}