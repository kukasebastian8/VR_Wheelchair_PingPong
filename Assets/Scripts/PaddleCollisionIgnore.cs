using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCollisionIgnore : MonoBehaviour
{
    public Collider paddle;
    public Collider Left_Wheel;
    public Collider Right_Wheel;
    public Collider Left_Caster;
    public Collider Right_Caster;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(paddle, Left_Wheel, true);
        Physics.IgnoreCollision(paddle, Right_Wheel, true);
        Physics.IgnoreCollision(paddle, Left_Caster, true);
        Physics.IgnoreCollision(paddle, Right_Caster, true);
    }
}
