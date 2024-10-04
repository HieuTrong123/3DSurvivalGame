using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeChopped;

    public float treeMaxhealth;
    public float treeHealth;

    private void Start(){
        treeHealth=treeMaxhealth;
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            playerInRange = false;
        }
    }
    public void GetHit(){
        StartCoroutine(hit());
    }

    public IEnumerator hit(){
        yield return new WaitForSeconds(0.6f);
        treeHealth-=1;
    }

    private void Update(){
        if(canBeChopped){
            GlobalState.Instance.resourceHealth=treeHealth;
            GlobalState.Instance.resourceMaxHealth=treeMaxhealth;
        }
    }
}
