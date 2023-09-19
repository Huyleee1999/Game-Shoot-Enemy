using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;

    public GameObject bullet;

    public Transform shootingPoint;

    GameController m_gc;
    
    public AudioSource aus;

    public AudioClip shootingSound;

    // Start is called before the first frame update
    void Start()
    {
        m_gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(m_gc.IsGameover()) {
            return;
        }

        float xDir = Input.GetAxisRaw("Horizontal");

        if((xDir < 0 && transform.position.x <= -9) || (xDir > 0 && transform.position.x >= 9))
        {
            return;
        }
        transform.position = transform.position + Vector3.right * moveSpeed * xDir * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot() 
    {
        if(bullet && shootingPoint)
        {
            if(aus && shootingSound)
            {
                aus.PlayOneShot(shootingSound);
            }

            Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
        {
            m_gc.SetGameoverState(true);

            Destroy(col.gameObject);

            Debug.Log("Enemy da va cham voi Player, tro choi ket thuc!");
        }
    }
}
