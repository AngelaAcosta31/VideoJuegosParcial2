using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MovimientoCarro : MonoBehaviour
{

    [Header("ID")]
    public int CarID = 0;
    public int checkPointsCrossed = 0;
    public int RacePosition;
    public int CurrentLap = 1;
    public bool isFinished;

    private GameManager gameManager;

    private Rigidbody rb;
    private RaycastHit[] hits;

    [Header("Configuración de Levitación")]
    [SerializeField] private float hoverHeight = 2.0f;
    [SerializeField] private float hoverForce = 10.0f;
    [SerializeField] private float stabilizationForce = 1.0f;
    [SerializeField] private Transform[] anchors;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float maxForwardSpeed = 100.0f;
    [SerializeField] private float accelerationRate = 10.0f;
    [SerializeField] private float decelerationRate = 5.0f;
    [SerializeField] private float maxTurnSpeed = 1.0f;
    [SerializeField] private Transform[] frontWheels;

    private float currentSpeed = 0.0f;
    private bool isGrounded = false;
    private PlayerInput playerInput;

    public TextMeshProUGUI lapTxt;
    public TextMeshProUGUI positionTxt;
    public TextMeshProUGUI TimerTxt;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hits = new RaycastHit[anchors.Length];
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerInput = GetComponent<PlayerInput>();
        lapTxt.text = $"Vuelta {CurrentLap}/{gameManager.m_nLaps}";
    }

    void Update(){
       //Debug.Log(currentSpeed);
       positionTxt.text = $"{RacePosition}.";
       TimerTxt.text = $"{gameManager.countdownTime}";
    }

    void FixedUpdate()
    {
        ApplyGravityCompensation();
        CheckGrounded();
        if (!isFinished){
            HandleMovement();
        }   
        ApplyHoverForces();
    }

    void ApplyGravityCompensation()
    {
        rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
    }

    void CheckGrounded()
    {
        isGrounded = false;
        foreach (Transform anchor in anchors)
        {
            RaycastHit hit;
            if (Physics.Raycast(anchor.position, -anchor.up, out hit, hoverHeight))
            {
                isGrounded = true;

                hits[System.Array.IndexOf(anchors, anchor)] = hit;
                break;
            }
        }
    }


    void HandleMovement()
    {
        float turnInput = playerInput.actions["Direction"].ReadValue<float>();
        float accelerateInput = playerInput.actions["Accelerate"].ReadValue<float>();
        float brakeInput = playerInput.actions["Brake"].ReadValue<float>();

        float turnAngle = turnInput *0.25f* maxTurnSpeed;

        HandleSpeed(accelerateInput, brakeInput);
        SetFrontWheelsAngle(turnAngle);
        ApplyMovement(turnAngle);
    }

    void HandleSpeed(float accelerateInput, float brakeInput)
    {
        if (brakeInput > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0.0f, decelerationRate * 3 * Time.fixedDeltaTime);
        }
        else if (accelerateInput > 0 && isGrounded)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxForwardSpeed, accelerationRate * Time.fixedDeltaTime);
        }
        else
        {
             currentSpeed = Mathf.MoveTowards(currentSpeed, 0.0f, decelerationRate * Time.fixedDeltaTime);
        }
    }

    void ApplyMovement(float turnAngle)
    {
        rb.AddRelativeTorque(Vector3.up * turnAngle, ForceMode.Acceleration);
        rb.AddRelativeForce(Vector3.forward * currentSpeed, ForceMode.Acceleration);
    }

    void SetFrontWheelsAngle(float turnAngle)
    {
        if (frontWheels == null || frontWheels.Length == 0)
            return;

        foreach (Transform frontWheel in frontWheels)
        {
            frontWheel.localEulerAngles = new Vector3(frontWheel.localEulerAngles.x, turnAngle*30, frontWheel.localEulerAngles.z);
        }
    }

    void ApplyHoverForces()
    {
        for (int i = 0; i < anchors.Length; i++)
        {
            ApplyHoverForce(anchors[i], i);
        }
    }

    void ApplyHoverForce(Transform anchor, int index)
    {
        bool hitSomething = Physics.Raycast(anchor.position, -anchor.up, out hits[index], hoverHeight);
        float rayLength = hitSomething? hits[index].distance : hoverHeight;

        Debug.DrawRay(anchor.position, -anchor.up * rayLength, Color.red);

        if (hitSomething)
        {
            float distance = hits[index].distance;
            float hoverError = hoverHeight - distance;

            if (rb.velocity.y < 0.5f && hoverError > 0)
            {
                float hoverForceToApply = hoverError * hoverForce;
                rb.AddForceAtPosition(Vector3.up * hoverForceToApply, anchor.position, ForceMode.Acceleration);
                Stabilize(anchor, hits[index]);
            }
        }
    }

    void Stabilize(Transform anchor, RaycastHit hit)
    {
        float stabilityError = Vector3.Dot(rb.transform.up, hit.normal) - 1f;
        float stabilizationTorque = stabilityError * stabilizationForce;
        rb.AddTorque(rb.transform.right * stabilizationTorque, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Ajustar la velocidad del carro en función de la fuerza y la dirección de la colisión
        float collisionForce = collision.impulse.magnitude;
        Vector3 collisionDirection = collision.contacts[0].normal;

        // Verificar si la colisión no ocurre en la parte inferior del vehículo
        if (Vector3.Angle(collisionDirection, Vector3.up) > 45f)
        {
            //Debug.Log(collisionDirection);
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, collisionForce * decelerationRate * Time.fixedDeltaTime);
            rb.AddForce(-collisionDirection * collisionForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint")){

            if (gameManager.isfinishedLap(CarID, checkPointsCrossed)){

                if (CurrentLap == gameManager.m_nLaps){
                    isFinished = true;
                    gameManager.finishRaceIfLast(RacePosition);

                } else{
                    checkPointsCrossed = 1;
                    CurrentLap += 1;
                    lapTxt.text = $"Vuelta {CurrentLap}/{gameManager.m_nLaps}";
                }

            } else{
                checkPointsCrossed += 1;
            }
            gameManager.CarCollectedCp(CarID,checkPointsCrossed);
        }
    }
}