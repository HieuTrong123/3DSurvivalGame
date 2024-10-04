using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    private Slider slider;
    public Text HydrationCounter;
    public GameObject PlayerState;
    private float currentHydration,maxHydration;
    // Start is called before the first frame update
    void Awake()
    {
        slider=GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        currentHydration=PlayerState.GetComponent<PlayerState>().currentHydrationPercent;
        maxHydration=PlayerState.GetComponent<PlayerState>().maxHydrationPercent;
        float fillValue=currentHydration/maxHydration;
        slider.value=fillValue;
        HydrationCounter.text=currentHydration+ "%";
    }
}
