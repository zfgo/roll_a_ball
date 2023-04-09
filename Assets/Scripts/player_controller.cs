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
    private float movement_x;
    private float movement_y;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        set_count_text();
        win_text_object.SetActive(false);
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

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movement_x, 0.0f, movement_y);
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
