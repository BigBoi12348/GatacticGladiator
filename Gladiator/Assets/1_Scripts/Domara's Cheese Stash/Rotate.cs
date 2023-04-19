using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Transform baseObj;
    [SerializeField] private Vector3 baseRotation;
    [SerializeField] private float basespeed;
    [SerializeField] private Transform TopObj;
    [SerializeField] private Vector3 topRotation;
    [SerializeField] private float topspeed;

    void Update()
    {
        baseObj.Rotate(baseRotation * basespeed * Time.deltaTime);
        TopObj.Rotate(topRotation * topspeed * Time.deltaTime);
    }
}
