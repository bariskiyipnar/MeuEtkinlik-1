using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class saldiriScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float hiz;
    public string yon;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,1.5f);
    }
    private void Update()
    {
        if(yon=="sol")
        {
            rb.velocity = transform.right*-1 * hiz;
        }
        else
        {
            rb.velocity = transform.right * hiz;
        }
    }
}