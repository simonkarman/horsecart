using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YouController : MonoBehaviour
{
    [SerializeField]
    private World world;
    
    [SerializeField]
    private int bread = 10;
    
    [SerializeField]
    private int gold = 10;

    [SerializeField]
    private TMP_Text breadText;
    
    [SerializeField]
    private TMP_Text goldText;
    
    protected void Update()
    {
        AxialCoordinate direction = AxialCoordinate.Zero;
        if (Input.GetKeyUp(KeyCode.Q)) {
            direction = AxialCoordinate.LeftUp;
        } else if (Input.GetKeyUp(KeyCode.W)) {
            direction = AxialCoordinate.Up;
        } else if (Input.GetKeyUp(KeyCode.E)) {
            direction = AxialCoordinate.RightUp;
        } else if (Input.GetKeyUp(KeyCode.D)) {
            direction = AxialCoordinate.RightDown;
        } else if (Input.GetKeyUp(KeyCode.S)) {
            direction = AxialCoordinate.Down;
        } else if (Input.GetKeyUp(KeyCode.A))
        {
            direction = AxialCoordinate.LeftDown;
        }

        var location = AxialCoordinate.FromPixel(transform.position, 0.63f).Rounded();
        if (direction != AxialCoordinate.Zero)
        {
            TryMoveTo(location + direction);
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            var settlement = world.GetSettlementAt(location);
            var breadCost = 0;
            var goldCost = 0;
            switch (settlement.GetSettlementType())
            {
                case SettlementType.Village:
                    breadCost = 1;
                    break;
                case SettlementType.City:
                    goldCost = 1;
                    break;
                case SettlementType.Town:
                default:
                    break;
            }

            if (bread < breadCost || gold < goldCost || !settlement.TryDepleteResources()) return;
            switch (settlement.GetSettlementType())
            {
                case SettlementType.Village:
                    gold += 3;
                    break;
                case SettlementType.City:
                    bread += 3;
                    break;
                case SettlementType.Town:
                default:
                    break;
            }
            bread -= breadCost;
            gold -= goldCost;
            UpdateUI();
        }
    }

    private void TryMoveTo(AxialCoordinate location)
    {
        var settlement = world.GetSettlementAt(location);
        if (settlement == null) {
            return;
        }

        var breadCost = settlement.GetSettlementType() == SettlementType.Village ? 2 : 1;
        var goldCost = (int)settlement.GetSettlementType();
        if (bread < breadCost || gold < goldCost) {
            return;
        }
        
        bread -= breadCost;
        gold -= goldCost;
        UpdateUI();
        SetLocation(location);
    }

    private void UpdateUI()
    {
        breadText.text = bread.ToString();
        goldText.text = gold.ToString();
    }

    public void SetLocation(AxialCoordinate location)
    {
        transform.position = location.ToPixel(0.63f);
    }
}
