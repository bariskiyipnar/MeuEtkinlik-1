using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class karakterScript : MonoBehaviour
{
    Rigidbody2D rb;
    bool zeminde,saldiriyor,hasarAldi,odunTus;
    public int maxZiplamaSayisi=2;
    int ziplamaSayisi=1;
    public float ziplamaKuvveti,hareketHizi, ziplamaAzaltma,isinMesafe;
    public LayerMask katman;
    Animator anim;
    public int odunSayisi,mermiSayisi,altinSayisi,dusmanSayisi,baslangicDusmanSayisi;
    public TextMeshProUGUI altintmp, mermitmp,sarttmp;
    public Vector3 kayitNoktasi;
    public GameObject saldiriObjesi;
    public Transform saldiriNoktasi;
    public AudioSource saldiriSes, hasarSes, ziplamaSes,asamaGecmeSes,altinToplamaSes,esyaToplamaSes;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
        baslangicDusmanSayisi= GameObject.FindGameObjectsWithTag("dusman").Length;
    }
    private void Update()
    {
        zeminKontrol();
        esyaKontrol();
        sartKontrol();
        if (saldiriyor == false && hasarAldi == false)
        {
            ziplamaKontrol();
            hareketKontrol();
            saldiriKontrol();
        }
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
    }
    void sartKontrol()
    {
        sarttmp.text = dusmanSayisi + "/" + baslangicDusmanSayisi;
        if (dusmanSayisi ==baslangicDusmanSayisi)
        {
            sarttmp.color = Color.green;
        }
        else
        {
            sarttmp.color = Color.red;
        }
    }
    void ziplamaKontrol()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            zipla();
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * ziplamaAzaltma);
        }
    }
    void saldiriKontrol()
    {
        if(mermiSayisi>0&&Input.GetMouseButtonDown(0))
        {
            saldiriyor = true;
            anim.SetTrigger("saldiri");
        }
    }
    void hareketKontrol()
    {
            float yon = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(yon * hareketHizi, rb.velocity.y);
            if (yon != 0)
            {
                anim.SetBool("yuruyor", true);
                if (yon < 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }
            else
            {
                anim.SetBool("yuruyor", false);
            }
    }
    public void esyaKontrol()
    {
        altintmp.text = "<sprite index=0>" + altinSayisi;
        mermitmp.text = "<sprite index=0>" + mermiSayisi;
    }
    void zipla()
    {
        if (zeminde ||((ziplamaSayisi < maxZiplamaSayisi)&&ziplamaSayisi>0))
        {
            ziplamaSes.Play();
            Invoke("ziplamaArttir",.1f);
            rb.velocity = new Vector2(rb.velocity.x, ziplamaKuvveti);
        }
    }
    void ziplamaArttir()
    {
        ziplamaSayisi++;
    }
    void zeminKontrol()
    {
        Vector2 solNokta = new Vector2(transform.position.x - 0.25f, transform.position.y);
        Vector2 sagNokta = new Vector2(transform.position.x + 0.25f, transform.position.y);
        RaycastHit2D solIsin = Physics2D.Raycast(solNokta, Vector2.down, isinMesafe, katman);
        RaycastHit2D sagIsin = Physics2D.Raycast(sagNokta, Vector2.down, isinMesafe, katman);
        zeminde = solIsin.collider != null || sagIsin.collider != null;
        if (zeminde)
        {
            ziplamaSayisi = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "altin")
        {
            altinSayisi++;
            altinToplamaSes.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "mermiObjesi")
        {
            mermiSayisi++;
            esyaToplamaSes.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "dusmanMermi")
        {
            hasarSes.Play();
            Destroy(collision.gameObject);
            anim.SetTrigger("hasar");
            hasarAldi = true;
        }
        else if (collision.gameObject.tag == "bayrak")
        {
            sarttmp.gameObject.SetActive(true);
            if (dusmanSayisi == baslangicDusmanSayisi)
            {
                asamaGecmeSes.Play();
                collision.gameObject.GetComponent<Animator>().SetTrigger("seviyeGecme");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bayrak")
        {
            sarttmp.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "kaybetme")
        {
            hasarSes.Play();
            anim.SetTrigger("hasar");
            hasarAldi = true;
        }
    }
    public void saldiriYap()
    {
        GameObject uretilenObje = Instantiate(saldiriObjesi);
        uretilenObje.transform.position = saldiriNoktasi.position;
        mermiSayisi--;
        saldiriSes.Play();
        if(new Vector3(0,180,0)==transform.localEulerAngles)
        {
            uretilenObje.GetComponent<saldiriScript>().yon = "sol";
        }
        else
        {
            uretilenObje.GetComponent<saldiriScript>().yon = "sað";
        }
    }
    public void saldiriBitir()
    {
        saldiriyor = false;
    }
    public void kaybetme()
    {
        GetComponent<karakterScript>().enabled = false;
        transform.position = kayitNoktasi;
        Invoke("aktiflestir",.2f);
    }
    public void aktiflestir()
    {
        GetComponent<karakterScript>().enabled = true;
        hasarAldi = false;
    }
}