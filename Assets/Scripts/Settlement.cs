using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SettlementType
{
    Village = 0,
    Town = 1,
    City = 2,
}

public class Settlement : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private AxialCoordinate location;
    
    [SerializeField, ReadOnly]
    private bool hasResources = true;
    
    [SerializeField, ReadOnly]
    private SettlementType type = SettlementType.Village;

    [SerializeField, ReadOnly]
    private SpriteRenderer spriteRenderer;

    public void Init(World world, AxialCoordinate location, SettlementType type)
    {
        this.location = location;
        this.type = type;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = world.SettlementColors[(int)type];
    }

    public bool TryDepleteResources()
    {
        if (!hasResources)
        {
            return false;
        }
        hasResources = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        return true;
    }

    public AxialCoordinate GetLocation()
    {
        return location;
    }

    public SettlementType GetSettlementType()
    {
        return type;
    }
}
