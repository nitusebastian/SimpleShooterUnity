using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController charCon;

    private Vector3 moveInput;
    [SerializeField] private Transform camTrans;
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private bool invertMouseX;
    [SerializeField] private bool invertMouseY;
    [SerializeField] private float minimumVert;
    [SerializeField] private float maximumVert;

    private float verticalRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        charCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput.Normalize();
        
        charCon.Move(moveInput * moveSpeed * Time.deltaTime);
        
        //control camera rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if (invertMouseX)
        {
            mouseInput.x = -mouseInput.x;
        }

        if (invertMouseY)
        {
            mouseInput.y = -mouseInput.y;
        }
        
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + mouseInput.x, transform.eulerAngles.z);

        verticalRot += mouseInput.y;
        verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
        //Vector3 rotationEulerAngles = camTrans.rotation.eulerAngles + new Vector3( mouseInput.y, 0f, 0f);
        camTrans.localEulerAngles = new Vector3(verticalRot, 0f, 0f);
        //camTrans.rotation = Quaternion.Euler(rotationEulerAngles);

    }
    
}
