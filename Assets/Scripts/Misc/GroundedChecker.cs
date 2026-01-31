using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    [SerializeField] float radius = 0.1f;
    [SerializeField] float verticalOffset = 0f;
    [SerializeField] LayerMask groundMask;
    public bool isGrounded {get; private set;}

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 origin = transform.position + new Vector3(0, verticalOffset, 0);

        isGrounded = Physics.CheckSphere(origin, radius, groundMask);
       
    }

    void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + new Vector3(0, verticalOffset, 0);

        Gizmos.color = isGrounded ? new Color(0f, 1f, 0f, 0.25f) : new Color(1f, 0f, 0f, 0.25f);

        Gizmos.DrawSphere(origin, radius);

    }

}