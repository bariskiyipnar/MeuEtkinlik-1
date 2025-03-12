using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class kayitNoktasiScript : MonoBehaviour
{
    public Vector3 kayitNoktasi;
    public GameObject player;
    Animator anim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim= GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<karakterScript>().kayitNoktasi = kayitNoktasi;
            player.GetComponent<karakterScript>().asamaGecmeSes.Play();
            anim.SetTrigger("kayit");
            Destroy(this);
        }
    }
}