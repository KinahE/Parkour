using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private GroundedChecker GroundedChecker;
    [SerializeField] private float drag = 0.3f;
    private Vector3 impact;
    private Vector3 dampingVelocity;
    private float verticalVelocity;
    public float Vel => verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    void Update()
    {
        if (verticalVelocity < 0f && GroundedChecker.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);


    }

    public void Jump(float jumpForce)
    {
        verticalVelocity = jumpForce;
    }


}
