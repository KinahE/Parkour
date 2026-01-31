using UnityEngine;

public class EnvironmentChecker : MonoBehaviour
{
    [SerializeField] Vector3 forwardRayOffset = new Vector3(0, 0.25f, 0);
    [SerializeField] float forwardRayLength = 0.8f;
    [SerializeField] float heightRayLength = 0.8f;
    [SerializeField] LayerMask obstacleLayer;

    private ObstacleHitData lastHitData;
    private bool hasHitData;
    
    public ObstacleHitData ObstacleCheck()
    {
        var hitData = new ObstacleHitData();
        var forwardOrigin = transform.position + forwardRayOffset;
        hitData.forwardHitFound = Physics.Raycast(forwardOrigin, transform.forward,
            out hitData.forwardHit, forwardRayLength, obstacleLayer);
        
        Debug.DrawRay(forwardOrigin, transform.forward * forwardRayLength, hitData.forwardHitFound? Color.red : Color.white);

        if(hitData.forwardHitFound)
        {
            var heightOrigin = hitData.forwardHit.point + transform.forward * 0.5f + Vector3.up * heightRayLength;
            hitData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down,
                out hitData.heightHit, heightRayLength, obstacleLayer);
            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, hitData.heightHitFound? Color.red : Color.white);
        }

        lastHitData = hitData;
        hasHitData = hitData.forwardHitFound && hitData.heightHitFound;

        return hitData;
    }

    private void OnDrawGizmos()
{
    if (!hasHitData) return;
    
    if (lastHitData.heightHitFound)
    {
        // Draw the actual hit point (top of obstacle - where FOOT will land)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(lastHitData.heightHit.point, 0.1f);
        
    }
}
}


