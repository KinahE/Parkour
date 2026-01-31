using UnityEngine;
using UnityEngine.Android;

[CreateAssetMenu(fileName = "ParkourAction", menuName = "Parkour System/ new ParkourAction")]
public class ParkourAction : ScriptableObject
{
    [SerializeField] string animationName;
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] bool rotateToObstacle;
    [SerializeField] bool enableTargetMatching = true;
    [SerializeField] AvatarTarget matchBodyPart;
    [SerializeField] float matchStartTime;
    [SerializeField] float matchTargetTime;
    public Quaternion TargetRotation {get; set;}
    public Vector3 MatchPosition {get; set;}
    public string AnimationName => animationName;
    public bool RotateToObstacle => rotateToObstacle;
    public bool  EnableTargetMatching => enableTargetMatching;
    public AvatarTarget MatchBodyPart => matchBodyPart;
    public float MatchStartTime => matchStartTime;
    public float MatchTargetTime => matchTargetTime;
    public bool CheckIfPossible(ObstacleHitData hitData, Transform player)
    {
        float height = hitData.heightHit.point.y - player.position.y;
        if (height < minHeight || height > maxHeight)
        {
            return false;
        }

        if (rotateToObstacle)
        {
            TargetRotation = Quaternion.LookRotation(-hitData.forwardHit.normal);
        }

        if (enableTargetMatching)
        {
            MatchPosition = hitData.heightHit.point;
        }

        return true;
    }

}
