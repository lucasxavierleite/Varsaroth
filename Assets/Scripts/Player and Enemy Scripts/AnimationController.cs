using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour 
{
    void Attack()
    {
        SendMessageUpwards("StartAttack");
    }

    void NoAttack()
    {
        SendMessageUpwards("StopAttack");
    }

    void Dodge()
    {
        SendMessageUpwards("StartDodge");
    }

    void NoDodge()
    {
        SendMessageUpwards("StopDodge");
    }

    void Invulnerability()
    {
        SendMessageUpwards("ToggleInvulnerability");
    }

    void Dead()
    {
        SendMessageUpwards("OnKill");
    }

}
