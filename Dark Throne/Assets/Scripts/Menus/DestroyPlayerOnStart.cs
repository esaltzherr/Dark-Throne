using UnityEngine;

public class DestroyPlayerOnStart : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
            Debug.Log("Player object destroyed.");
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }
}
