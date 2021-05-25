using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class LaserPointer : MonoBehaviour
{

    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Input_Sources hand;
    private LineRenderer line;

    private SteamVR_Action_Boolean trigger;


    private SteamVR_Action_Boolean teleport;


    //Line Max Distance
    public float maxDistance = 30.0f;

    //Line Color
    public Color color = Color.blue;
    public Color clickedColor = Color.green;


    private RaycastHit hit;
    private Transform tr;

    private GameObject pointerPrefab;

    private GameObject pointer;

    public float fadeOutTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        trigger = SteamVR_Actions.default_InteractUI;
        teleport = SteamVR_Actions.default_Teleport;




        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
        tr = GetComponent<Transform>();

        CreateLine();

        pointerPrefab = Resources.Load<GameObject>("Pointer");

        pointer = Instantiate<GameObject>(pointerPrefab);

    }

    void CreateLine()
    {
        line = this.gameObject.AddComponent<LineRenderer>();

        line.useWorldSpace = false;

        // Start, End Position Setting.
        line.positionCount = 2; 
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, new Vector3 (0, 0, maxDistance));

        // Line Width Setting
        line.startWidth = 0.03f; // 3cm
        line.endWidth = 0.005f; // 0.5cm

        // Materials setting
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = this.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(tr.position, tr.forward, out hit, maxDistance))
        {
            line.SetPosition(1, new Vector3(0, 0, hit.distance));
            // z-fighting 을 방지하기위해 0.01 만큼 앞으로 이동
            pointer.transform.position = hit.point + (hit.normal * 0.01f);
            pointer.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            line.SetPosition(1, new Vector3(0, 0, maxDistance));
            //if raycast hits nothing, set pointer max distance.
            pointer.transform.position = tr.position + (tr.forward * maxDistance);
            pointer.transform.rotation = Quaternion.LookRotation(tr.forward);
        }

        // When Click Teleport
        if(teleport.GetStateDown(hand) || Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(tr.position, tr.forward, out hit, maxDistance, 1<<8))
            {
                SteamVR_Fade.Start(Color.black, 0); //(newColor, duration)
                StartCoroutine(Teleport(hit.point));
            }
        }
    }

    IEnumerator Teleport(Vector3 pos)
    {
        tr.parent.position = pos;

        yield return new WaitForSeconds(fadeOutTime);

        SteamVR_Fade.Start(Color.clear, 0.3f);
    }
}
