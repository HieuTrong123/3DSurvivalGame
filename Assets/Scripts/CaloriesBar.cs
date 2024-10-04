using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesBar : MonoBehaviour
{
    private Slider slider;
    public Text CaloriesCounter;
    public GameObject PlayerState;
    private float currentCalories,maxCalories;
    // Start is called before the first frame update
    void Awake()
    {
        slider=GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        currentCalories=PlayerState.GetComponent<PlayerState>().currentCalories;
        maxCalories=PlayerState.GetComponent<PlayerState>().maxCalories;
        float fillValue=currentCalories/maxCalories;
        slider.value=fillValue;
        CaloriesCounter.text=currentCalories+ "/" + maxCalories;
    }
}
