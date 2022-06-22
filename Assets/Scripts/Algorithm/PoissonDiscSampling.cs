using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoissonDiscSampling
{
    public static List<Vector2> GeneratePoints(float cellRadius, Vector2 sampleRegionSize, int amount = -1, int numSamplesBeforeRejection = 20)
    {
        // Calculate length of the sides of the cell
        float cellSize = cellRadius / Mathf.Sqrt(2);  // r^2 = s1^2 + s2^2
                                                      // r^2 = 2s^2
                                                      // s^2 = r^2/2
                                                      // s = sqrt(r^2 / 2)
                                                      // s = r / sqrt(2)
                                                      // (where r==radius, s1,s2==sides)

        // Create grid of {sampleRegionSize} size containing cells of {cellsize} size
        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x / cellSize), Mathf.CeilToInt(sampleRegionSize.y / cellSize)];

        // List of all points
        List<Vector2> points = new List<Vector2>();

        // List of points new points can spawn from
        List<Vector2> spawnPoints = new List<Vector2>();

        // Initiate spawnPoints with a point in the middle of the grid
        spawnPoints.Add(sampleRegionSize / 2);

        while (spawnPoints.Count > 0 && (amount == -1 || points.Count < amount))
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count - 1);
            Vector2 spawnCentre = spawnPoints[spawnIndex];

            bool candidateAccepted = false;
            for (int i = 0; i < numSamplesBeforeRejection; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCentre + dir * Random.Range(cellRadius, 2 * cellRadius);
                if (!IsValid(candidate, sampleRegionSize, cellSize, cellRadius, points, grid))
                {
                    continue;
                }
                points.Add(candidate);
                spawnPoints.Add(candidate);
                grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                candidateAccepted = true;
                break;
            }

            // Failed to find candidate
            if (!candidateAccepted)
            {
                spawnPoints.RemoveAt(spawnIndex);
            }
        }

        return points;
    }

    private static bool IsValid(Vector2 candidatePoint, Vector2 sampleRegionSize, float cellSize, float cellRadius, List<Vector2> points, int[,] grid)
    {
        if (!(candidatePoint.x >= 0 && candidatePoint.x < sampleRegionSize.x && candidatePoint.y >= 0 && candidatePoint.y < sampleRegionSize.y))
        {
            return false;
        }
        int cellX = (int)(candidatePoint.x / cellSize);
        int cellY = (int)(candidatePoint.y / cellSize);
        int searchStartX = Mathf.Max(0, cellX - 2);
        int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
        int searchStartY = Mathf.Max(0, cellY - 2);
        int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

        for (int x = searchStartX; x <= searchEndX; x++)
        {
            for (int y = searchStartY; y <= searchEndY; y++)
            {
                int pointIndex = grid[x, y] - 1;

                // if (pointIndex == -1) continue;
                if (pointIndex != -1)
                {
                    float sqrDistance = (candidatePoint - points[pointIndex]).sqrMagnitude;
                    if (sqrDistance < (cellRadius * cellRadius))
                    {
                        // Candidate is too close to the point
                        return false;
                    }
                }
            }
        }

        return true;
    }
}