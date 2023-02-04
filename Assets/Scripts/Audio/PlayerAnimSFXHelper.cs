using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimSFXHelper : MonoBehaviour
{
    public PlayerSFX playerSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPlayerSFX(PlayerSFX.ClipName clip)
    {
        playerSFX.playPlayerSFX(clip);
    }
}
