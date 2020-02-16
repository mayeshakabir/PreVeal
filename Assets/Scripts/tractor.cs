﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tractor : MonoBehaviour
{
    public float speed;
    public GameObject potato;
    public float timeLeft;

    private int potatoes;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        potatoes = 3;
        score = 0;
    }



    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;      
    
        //new_potato.GetComponent<RigibBody2D>().velocty = Vector.up;

        //getting position with which button
        //returns -1 - 1, on which pos/neg button you press


     
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow)) && GameOver.isInputEnabled)
        {

            //Movement with what button direction * speed I want * Time.deltaTime to make it run per second
            Vector2 movement = new Vector2(1, 0) * speed * Time.deltaTime;

            //the movement
            transform.Translate(movement);

        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.LeftArrow)) && GameOver.isInputEnabled)
        {

            //Movement with what button direction * speed I want * Time.deltaTime to make it run per second
            Vector2 movement = new Vector2(-1, 0) * speed * Time.deltaTime;

            //the movement
            transform.Translate(movement);

        }
        

        //bounds
        var dist = (transform.position - Camera.main.transform.position).z;

        var left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var top = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, left, right),
            Mathf.Clamp(transform.position.y, top, bottom),
            transform.position.z);


        if (Input.GetKeyDown(KeyCode.UpArrow) && potatoes > 0 && timeLeft < 0  && GameOver.isInputEnabled)
        {
            Instantiate(potato, transform.position, Quaternion.identity);
            potatoes -= 1;
            timeLeft = 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<FollowerCow>() != null)
        {
            ParticleSystem explosion = Instantiate(other.gameObject.GetComponent<FollowerCow>().bloodBomb);
            explosion.transform.position = transform.position;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "potato_sack")
        {
            potatoes += 5;
            Destroy(col.gameObject);
        }
        else if (col.tag == "mud")
        {
            if (potatoes > 2) {
                potatoes -= 3;
            }
            else
            {
                potatoes = 0;
            }
            
        }
        else if (col.tag == "veal") {
            score++;
            Destroy(col.gameObject);
        }
    }

    public int getScore() 
    {
        return score;
    }

    public int getPotatoCount() 
    {
        return potatoes;
    }
}
