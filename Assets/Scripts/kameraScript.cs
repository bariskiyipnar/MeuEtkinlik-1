using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class kameraScript : MonoBehaviour
{
    Vector3 fark;
    Transform hedef;
    public float minX, maxX;
    void Start()
    {
        hedef = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void LateUpdate()
    {
        float xpos = hedef.position.x;
        float yeniX = Mathf.Clamp(xpos,minX,maxX);
        transform.position = new Vector3(yeniX,transform.position.y,transform.position.z);
    }
}