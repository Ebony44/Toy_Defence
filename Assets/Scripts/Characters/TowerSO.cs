using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Tower Configuration", menuName = "ScriptableObject/Tower Configuration")]
public class TowerSO : SerializableScriptableObject
{
    public int Health = 100;

    public float AttackDelay = 1f;
    public int Damage = 5;
    public float AttackRadius = 6f;


    public float AIUpdateInterval = 0.1f;
    public float Acceleration = 8;
    public float AngularSpeed = 120;
    public int AreaMask = -1;
    public int AvoidancePriority = 50;
    public float BaseOffset = 0;
    public float Height = 2f;
    
    public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float Radius = 0.5f;
    // public float Speed = 3f;
    public float StoppingDistance = 0.5f; // if tower can move

}
