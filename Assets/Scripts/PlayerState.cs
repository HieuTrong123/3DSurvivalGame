using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance {get; set;}

    public float currentHealth;
    public float maxHealth;
    
    public float currentCalories;
    public float maxCalories;

    float distanceTravelled=0;
    Vector3 lastPosition;

    public GameObject playerBody;

    public float currentHydrationPercent;
    public float maxHydrationPercent;

    public bool isHydrationActive;

    public void setHealth(float health){
        this.currentHealth=health;
    }
    public void setCalories(float calories){
        this.currentCalories=calories;
    }
    public void setHydration(float hydration){
        this.currentHydrationPercent=hydration;
    }

    private void Awake(){
        if(Instance!= null && Instance!=this){
            Destroy(gameObject);

        }
        else{
            Instance=this;
        }
    }

    private void Start(){
        currentHealth=maxHealth;
        currentCalories=maxCalories;
        currentHydrationPercent=maxHydrationPercent;
        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration(){
        while(true){
            currentHydrationPercent-=1;
            yield return new WaitForSeconds(10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled+=Vector3.Distance(playerBody.transform.position,lastPosition);
        lastPosition=playerBody.transform.position;

        if(distanceTravelled>=5){
            distanceTravelled=0;
            currentCalories-=1;
        }
        if(Input.GetKeyDown(KeyCode.N)){
            currentHealth-=10;
        }
    }
}
