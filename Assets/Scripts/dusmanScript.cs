using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class dusmanScript : MonoBehaviour
{
    Transform hedef;
    public float mesafe,beklemeSuresi,beklemeSuresiBaslangic;
    Animator anim;
    bool hasarAldi;
    public GameObject saldiriObjesi;
    public Transform saldiriNoktasi;
    public AudioSource saldiriSes, hasarSes;
    public string yon;
    void Start()
    {
        hedef = GameObject.FindGameObjectWithTag("Player").transform;
        anim= GetComponent<Animator>();
    }
    void Update()
    {
        if(Vector3.Distance(transform.position,hedef.position)<=mesafe)
        {
          if(transform.position.x>=hedef.position.x)
            {
                transform.rotation= Quaternion.identity;
                yon = "sol";
            }
          else
            {
                transform.rotation = Quaternion.Euler(0,180,0);
                yon = "sað";
            }
          if(beklemeSuresi<=0&&hasarAldi==false)
            {
                beklemeSuresi = beklemeSuresiBaslangic;
                anim.SetTrigger("saldiri");
            }
          else
            {
                beklemeSuresi -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="mermi")
        {
            hasarSes.Play();
            Destroy(collision.gameObject);
            hasarAldi = true;
            anim.SetTrigger("hasar");
        }
    }
    public void yokEt()
    {
        hedef.GetComponent<karakterScript>().dusmanSayisi++;
        Destroy(gameObject);
    }
    public void saldiriYap()
    {
        saldiriSes.Play();  
        GameObject uretilenObje = Instantiate(saldiriObjesi);
        uretilenObje.transform.position = saldiriNoktasi.position;
        uretilenObje.GetComponent<saldiriScript>().yon = yon;
    }
}