using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


namespace Script
{
    public class Prize : MonoBehaviour
    {
        
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
            GameManager.instance.score++;

        }
    }
}
