using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move1 : MonoBehaviour
{


    public Vector2 SPEED = new Vector2(0.05f, 0.05f);
    Vector2 Position;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Player_Move();
    }
    /*
     * -------------------------
     Player移動関数* 
     ---------------------------*/

    void Player_Move()
    {
        Position = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Rotate(new Vector3(0, 0, -90));
            }
            Position.x -= SPEED.x;
            transform.position = Position;
        }
        transform.position = Position;

        if (Input.GetKey(KeyCode.D))
        {
            //Dキーが押されたら右に動く
            Position.x += SPEED.x;
            transform.position = Position;
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(new Vector3(0, 0, 90));
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            Position.y += SPEED.y;
            transform.position = Position;
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Rotate(new Vector3(0, 0, -180));
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            Position.y -= SPEED.y;
            transform.position = Position;
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.Rotate(new Vector3(0, 0, 180));
            }

        }


    }


}
