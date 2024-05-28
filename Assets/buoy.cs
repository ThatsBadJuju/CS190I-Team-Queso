using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buoy : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;

    void Start()
    {
        score = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Human"))
        {
            score += 1;
        }
    }
}
