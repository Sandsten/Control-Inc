using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(AngleToPlayer());
    }

    float AngleToPlayer(){
        float angle = 0;

        Vector3 enemyLookDir = gameObject.transform.forward;
        Vector3 dirToPlayer = player.transform.position - gameObject.transform.position;

        angle = Vector3.Angle(enemyLookDir, dirToPlayer.normalized);

        return angle;
    }
}
