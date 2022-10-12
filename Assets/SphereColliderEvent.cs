using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColliderEvent : MonoBehaviour
{
    public Transform HitTransform
    {
        set
        {
            this._hitTransform = value;
        }

        get
        {
            return this._hitTransform;
        }
    }

    private Transform _hitTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sphereにキューブが接触したとき
    void OnTriggerEnter(Collider collider)
    {
        HitTransform = collider.transform;
    }

    //sphereにキューブが接触するのをやめたとき
    void OnTriggerExit(Collider collider)
    {
        HitTransform = null;
    }
}
