using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_movement : MonoBehaviour
{

    // PLAYER MOVEMENT
    // variables for player movement
    [SerializeField]
    public float _moveSpeed = 7f;

    [SerializeField]
    public float _turnSpeed = 2f;

    private Vector3[] _startposition = { new Vector3(280.1f, 21.4f, 562.3f), new Vector3(378.2f, 21.31f, 875.6f), new Vector3(642.5f, 33.16f, 407.1f) };

    private Vector3[] _startrotation = { new Vector3(0, 0, 0), new Vector3(0, 90f, 0), new Vector3(0, 190f, 0) };
    // Start is called before the first frame update
    void Start()
    {
        // randomised start position
        int index = Random.Range(0, 2);
        Vector3 position = _startposition[index];
        
        // set start position & rotate in the correct direction
        transform.position = position;
        transform.Rotate(_startrotation[index]);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //  ROTATION - horizontal player movement: rotates the player on the y-axis
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0)
        {
            transform.Rotate(0, horizontal * _turnSpeed, 0);
        }
        else if (horizontal < 0)
        {
            transform.Rotate(0, horizontal * _turnSpeed, 0);
        }

        // MOVEMENT - vertical player movement: moves player forward or back
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
        else if (vertical < 0)
        {
            transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
        }
    }
    
}
