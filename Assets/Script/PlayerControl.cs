using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float forceAmount = 5;
    private Rigidbody2D rb2d;

    public static PlayerControl instance;
    
    public GameObject projectilePrefab;
    public float horizontalInput;
    public float speed= 5 ;
    
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(Vector2.right*forceAmount);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddForce(Vector2.left*forceAmount);
        }
        
        horizontalInput = Input.GetAxis("Horizontal");
        

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
