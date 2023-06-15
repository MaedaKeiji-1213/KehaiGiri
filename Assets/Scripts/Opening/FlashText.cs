using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]float flash_interval=0f;
    TextMeshProUGUI text_mesh;
    float flash_start_time;
    void Start()
    {
        text_mesh=GetComponent<TextMeshProUGUI>();
    }
    void OnEnabled()
    {
        flash_start_time=Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        Color color=text_mesh.color;
        color.a=Mathf.Sin((Time.time-flash_start_time+flash_interval/2)*Mathf.PI/flash_interval)/2+0.5f;
        //Debug.Log("alpha:"+color.a);
        text_mesh.color=color;
    }
}
