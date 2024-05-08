using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    private bool doubleJumpAquired = false;
    private static float maxJumps = 1;
    private float jumps = maxJumps;

    void Update(){
        if (Input.GetKeyDown(KeyCode.N)){
            toggleDoubleJump();
        }
    }

    public bool CanDoubleJump() 
    {
        return doubleJumpAquired && jumps > 0; 
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
    
    public void gainDoubleJump(){
        doubleJumpAquired = true;
    }

    public void loseDoubleJump(){
        doubleJumpAquired = false;
    }

    public void toggleDoubleJump(){
        if (doubleJumpAquired){
            loseDoubleJump();
        }
        else{
            gainDoubleJump();
        }
    }

    public bool doubleJumpGained(){
        return doubleJumpAquired;
    }
    public void setDoubleJumpGained(bool aquired){
        doubleJumpAquired = aquired;
    }

}
