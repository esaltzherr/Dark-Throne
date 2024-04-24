using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    private bool doubleJumpAvailable = true;
    private static float maxJumps = 1;
    private float jumps = maxJumps;


    public bool CanDoubleJump() 
    {
        return doubleJumpAvailable && jumps > 0; 
    }

    public void DoubleJump()
    {
        if (jumps > 0)
        {
            jumps--;
        }
    }

    public void ResetDoubleJump()
    {
        jumps = maxJumps;
    }
    
}
