using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.Extras;


public class CastEventToUI : MonoBehaviour
{

    private SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
    }

    //Connect event function
    void OnEnable()
    {
        laserPointer.PointerIn += this.OnPointerEnter;
        laserPointer.PointerOut += this.OnPointerExit;
        laserPointer.PointerClick += this.OnPointerClick;

    }
    
    //Disconnect event function
    void OnDisable()
    {

        laserPointer.PointerIn += this.OnPointerEnter;
        laserPointer.PointerOut += this.OnPointerExit;
        laserPointer.PointerClick += this.OnPointerClick;
    }

    void OnPointerEnter(object sender, PointerEventArgs e)
    {
        var enterHandler = e.target.GetComponent<IPointerEnterHandler>();

        if(enterHandler == null) return;

        enterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
    }

    void OnPointerExit(object sender, PointerEventArgs e)
    {
        var exitHandler = e.target.GetComponent<IPointerExitHandler>();

        if(exitHandler == null) return;

        exitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
    }

    void OnPointerClick(object sender, PointerEventArgs e)
    {
        var clickHandler = e.target.GetComponent<IPointerClickHandler>();

        if(clickHandler == null) return;

        clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
        Debug.Log("aaa");
    }
}
