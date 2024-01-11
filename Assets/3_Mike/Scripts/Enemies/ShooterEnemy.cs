using UnityEngine;

public class ShooterEnemy : MachineGunEnemy
{
    [SerializeField] private LineRenderer lineRenderer; // Assign in the Inspector the LineRenderer component
    [SerializeField] private Transform lineSpawnPoint;

    // Constructor modified to pass bulletSpawnPoints to the base class (MeleeEnemy)
    public ShooterEnemy(float _attackRange, float _attackTime, Transform[] _bulletSpawnPoints)
        : base(_attackRange, _attackTime, _bulletSpawnPoints)
    {}


    protected override void Start()
    {
        base.Start();

        lineRenderer.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        
        // Draw a line towards the shooting target if within attack range
        DrawLineToTarget();
    }

    private void DrawLineToTarget()
    {
        if (target != null) //Checking for null before targeting
        {
            if (lineRenderer != null && target != null && target.CompareTag("Player"))
            {
                float distanceToTarget = Vector2.Distance(lineSpawnPoint.position, target.position);

                if (distanceToTarget <= attackRange)
                {   
                    lineRenderer.gameObject.SetActive(true);
                    Vector3 lineEndPosition = target.position;
                    lineRenderer.positionCount = 2; // Set the number of positions for the line
                    lineRenderer.SetPosition(0, lineSpawnPoint.position); // Set the starting point of the line to the enemy position
                    lineRenderer.SetPosition(1, lineEndPosition); // Set the end point of the line
                }
                else
                {
                    lineRenderer.positionCount = 0;
                }
            }
        }
        else
        {
            lineRenderer.gameObject.SetActive(false);
        }
    }
}
