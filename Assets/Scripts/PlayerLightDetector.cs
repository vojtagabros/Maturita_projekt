using UnityEngine;

public class AreaLightDetector : MonoBehaviour
{
    public Light areaLight;
    public float maxDistance = 15f;
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;

    void Update()
    {
        Collider[] enemies = Physics.OverlapSphere(areaLight.transform.position, maxDistance, enemyLayer);

        foreach (Collider enemyCol in enemies)
        {
            Transform enemy = enemyCol.transform;
            Vector3 direction = enemy.position - areaLight.transform.position;
            float distance = direction.magnitude;

            if (distance > maxDistance)
            {
                SetEnemyVisible(enemy, false);
                continue;
            }

            direction.Normalize();

            // Raycast to check obstacle
            if (!Physics.Raycast(areaLight.transform.position, direction, distance, obstacleLayer))
            {
                SetEnemyVisible(enemy, true);
            }
            else
            {
                SetEnemyVisible(enemy, false);
            }
        }
    }

    void SetEnemyVisible(Transform enemy, bool visible)
    {
        var vis = enemy.GetComponent<EnemyVisibility>();
        if (vis != null)
            vis.SetVisible(visible);
    }
}