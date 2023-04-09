using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class player_controller : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI count_text;
    public GameObject win_text_object;

    private Rigidbody rb;
    private int count;
    private bool if_jump;
    private int jump_count;
    private bool in_air;
    private int cur_jump_count;
    private float movement_x;
    private float movement_y;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        set_count_text();
        win_text_object.SetActive(false);
        if_jump = false;
        in_air = false;
        jump_count = 2;
        cur_jump_count = jump_count;
    }

    void OnMove(InputValue movement_value)
    {
        Vector2 movement_vector = movement_value.Get<Vector2>();

        movement_x = movement_vector.x;
        movement_y = movement_vector.y;
    }

    void set_count_text()
    {
        count_text.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            win_text_object.SetActive(true);
        }
    }

    void Update()
    {
        if_jump |= Input.GetButtonDown("Jump");
        if (cur_jump_count == 0)
        {
            cur_jump_count = jump_count;
        }
    }

    void jump()
    {
        if_jump = false;
        cur_jump_count--;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movement_x, 0.0f, movement_y);

        if (if_jump && rb.velocity.y == 0)
        {
            jump();
            movement.y += 25.0f;
            in_air = true;
        }
        else if (if_jump && cur_jump_count > 0 && in_air)
        {
            jump();
            movement.y += 25.0f;
            if (cur_jump_count < 1)
            {
                in_air = false;
            }
        }

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            set_count_text();
        }
    }
}
