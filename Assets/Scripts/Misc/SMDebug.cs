using UnityEngine;
using TMPro;

public class SMDebug : MonoBehaviour
{
    [SerializeField] Player_SM SM;
    public TMP_Text debugText;


    // Update is called once per frame
    void Update()
    {
        if(SM)
        {
            debugText.text = $"Is Grounded: {SM.GroundedChecker.isGrounded.ToString()}";
        }
    }
}
// \n Vertical Velocity: {SM.ForceReceiver.Vel.ToString()}