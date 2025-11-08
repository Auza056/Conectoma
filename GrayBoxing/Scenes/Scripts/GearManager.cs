using UnityEngine;
using System.Collections.Generic;

public class GearManager : MonoBehaviour
{
    [Header("Engranajes")]
    public List<Gear> gears = new List<Gear>();
    public Gear startGear;
    public float neighborDistance = 0.75f; // distancia máxima para considerar contacto
    public KeyCode activateKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(activateKey))
        {
            if (startGear == null)
            {
                Debug.LogWarning("StartGear no asignado en GearManager.");
                return;
            }

            HashSet<Gear> visited = new HashSet<Gear>();
            RotateChain(startGear, true, visited);

            if (visited.Count == CountPlacedGears())
            {
                Debug.Log("Puzzle: todos los engranajes conectados y rotando.");
                // aquí podrías llamar a OnPuzzleComplete();
            }
            else
            {
                Debug.Log($"Puzzle: rotaron {visited.Count} de {CountPlacedGears()} engranajes colocados.");
            }
        }
    }

    int CountPlacedGears()
    {
        int c = 0;
        foreach (var g in gears) if (g != null && g.isPlaced) c++;
        return c;
    }

    void RotateChain(Gear gear, bool clockwise, HashSet<Gear> visited)
    {
        if (gear == null || visited.Contains(gear)) return;
        if (!gear.isPlaced) return; // solamente los colocados participan en la cadena

        visited.Add(gear);
        gear.Rotate(clockwise);

        // buscar vecinos basados en distancia entre centros
        foreach (var other in gears)
        {
            if (other == null || other == gear) continue;
            if (!other.isPlaced) continue;

            float dist = Vector3.Distance(gear.transform.position, other.transform.position);
            if (dist <= neighborDistance)
            {
                RotateChain(other, !clockwise, visited);
            }
        }
    }

    // debug visual de conexiones
    void OnDrawGizmos()
    {
        if (gears == null) return;
        Gizmos.color = Color.magenta;
        foreach (var g in gears)
        {
            if (g == null || !g.isPlaced) continue;
            foreach (var o in gears)
            {
                if (o == null || !o.isPlaced || o == g) continue;
                float dist = Vector3.Distance(g.transform.position, o.transform.position);
                if (dist <= neighborDistance)
                {
                    Gizmos.DrawLine(g.transform.position, o.transform.position);
                }
            }
        }
    }
}
