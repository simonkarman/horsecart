using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SettlementType
{
    Village = 0,
    Town = 1,
    City = 2,
}

public class Settlement : MonoBehaviour
{
    [SerializeField]
    private AxialCoordinate coord;
    
    [SerializeField]
    private SettlementType type = SettlementType.Village;

    public void Init(World world, AxialCoordinate coord, SettlementType type)
    {
        this.coord = coord;
        this.type = type;
        GetComponent<SpriteRenderer>().color = world.SettlementColors[(int)type];
    }
}
