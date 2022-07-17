using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class World : MonoBehaviour
{
    [SerializeField]
    private Color[] settlementColors = new[] { Color.green, Color.yellow, Color.gray };
    public Color[] SettlementColors => settlementColors;

    [SerializeField]
    private Settlement settlementPrefab;

    [SerializeField]
    private int worldSize = 10;
    
    [SerializeField]
    private string worldSeed = "abc";

    [SerializeField]
    private YouController you;

    [SerializeField]
    private List<AxialCoordinate> coords;

    [ContextMenu("Generate")]
    protected void Generate()
    {
        var rand = new System.Random(worldSeed.GetHashCode());
        
        // Destroy all children
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        
        // Shape the world coords
        var open = new List<AxialCoordinate>();
        coords.Clear();
        open.Add(AxialCoordinate.Zero);
        while (coords.Count < worldSize)
        {
            var index = rand.Next(open.Count - 1);
            var coord = open[index];
            coords.Add(coord);
            open.RemoveAt(index);
            foreach (var ac in AxialCoordinate.Directions)
            {
                var neighbor = coord + ac;
                if (open.Contains(neighbor) || coords.Contains(neighbor))
                {
                    continue;
                }
                open.Add(neighbor);
            }
        }
        
        // Spawn settlements
        foreach (var coord in coords)
        {
            Settlement settlement = Instantiate(settlementPrefab, coord.ToPixel(0.63f), Quaternion.identity, transform).GetComponent<Settlement>();
            var settlementStength = rand.Next(10);
            var settlementType = settlementStength < 5
                ? SettlementType.Village
                : (settlementStength < 8 ? SettlementType.Town : SettlementType.City);
            settlement.Init(this, coord, settlementType);
            settlement.name = "Settlement " + settlementType.ToString();
        }
        
        // Spawn you
        you.SetLocation(coords.Last());
    }

    public bool HasTileAt(AxialCoordinate location)
    {
        return coords.Contains(location);
    }
}
