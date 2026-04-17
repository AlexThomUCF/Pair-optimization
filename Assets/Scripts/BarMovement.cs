using UnityEngine;
using UnityEngine.InputSystem;

public class BarMovement : MonoBehaviour
{
    public float speed = 5f;
    public float maxX = 7.5f;

    private PlayerControls controls;
    private float movementHorizontal;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => movementHorizontal = ctx.ReadValue<float>();
        controls.Player.Move.canceled += ctx => movementHorizontal = 0f;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if((movementHorizontal > 0 && transform.position.x < maxX) || (movementHorizontal <0 && transform.position.x > -maxX))
        {
            transform.position += Vector3.right * movementHorizontal * speed * Time.deltaTime;
        }
        
    }
}
