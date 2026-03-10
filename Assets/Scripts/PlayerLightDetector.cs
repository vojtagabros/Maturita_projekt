using UnityEngine;

public class AreaLightDetector : MonoBehaviour
{
    public Light areaLight;
    public float maxDistance = 15f;
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;

    private float _checkInterval = 0.15f;
    private float _timer = 0f;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _checkInterval)
            return;
        _timer = 0f;

        Collider[] enemies = Physics.OverlapSphere(areaLight.transform.position, maxDistance, enemyLayer);

        foreach (Collider enemyCol in enemies)
        {
            Transform enemy = enemyCol.transform;
            Vector3 direction = enemy.position - areaLight.transform.position;
            float distance = direction.magnitude;

            if (!Physics.Raycast(areaLight.transform.position, direction.normalized, distance, obstacleLayer))
                SetEnemyVisible(enemy, true);
            else
                SetEnemyVisible(enemy, false);
        }
    }

    void SetEnemyVisible(Transform enemy, bool visible)
    {
        var vis = enemy.GetComponent<EnemyVisibility>();
        if (vis != null)
            vis.SetVisible(visible);
    }
}
