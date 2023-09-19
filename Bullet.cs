using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D m_rd;

    public float speed;

    public float timeToDestroy;

    AudioSource aus;

    public AudioClip hitSound;

    GameController m_gc;

    public GameObject hitVFX;

    // Start is called before the first frame update
    void Start()
    {
        m_rd = GetComponent<Rigidbody2D>();

        m_gc = FindObjectOfType<GameController>();

        aus = FindObjectOfType<AudioSource>();

        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        m_rd.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            //Tang diem so cua nguoi choi khi vien dan va cham enemy
            m_gc.IncrementScore();

            if(aus && hitSound)
            {
                aus.PlayOneShot(hitSound);
            }

            if(hitVFX) 
            {
                Instantiate(hitVFX, col.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);

            Destroy(col.gameObject);

            Debug.Log("Vien dan da va cham voi Enemy!");
        } else if(CompareTag("SceneTopLimit")) 
        {
            Destroy(gameObject);
        }
    }
}
