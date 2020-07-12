using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class FieldOfViewEditor : Editor
{

    private void OnSceneGUI() {
        // Get the object that this is a custom editor of with "target"
        Enemy enemy = target as Enemy;

        Vector3 playerPos = enemy.player.transform.position;
        Vector3 enemyPos = enemy.transform.position;
        Vector2 enemyPos2D = new Vector2(enemyPos.x, enemyPos.y);

        // Draw a line between the enemy and the player
        if(enemy.IsPlayerSpotted())
            Handles.color = Color.red;  // If it's red, the player has been spotted
        else
            Handles.color = Color.green;
        
        Vector3 playerDir = (playerPos - enemyPos).normalized;
        Handles.DrawLine(enemyPos, enemyPos + playerDir * enemy.viewRadius);

        // Draw the enemy's view radius
        Handles.color = Color.cyan;
        Handles.DrawWireArc(enemyPos,Vector3.forward, Vector3.up, 360, enemy.viewRadius);
        
        // Draw a line in the enemy's move direction
        Handles.DrawLine(enemyPos, enemyPos2D + enemy.moveDirection.normalized * enemy.viewRadius);
    }

}
