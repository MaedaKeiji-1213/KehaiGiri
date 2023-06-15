using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataContainer : MonoBehaviour
{
    public static GameDataContainer instance;
    [SerializeField]GameObject[] players;
    public GameObject[] Players{get{return players;}}
    void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        instance=this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
