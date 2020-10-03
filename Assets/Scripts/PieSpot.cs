using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieSpot : MonoBehaviour
{
    public bool IsProtected = false;
    public Sprite ProtectionSprite = null;
    public Sprite NormalSprite = null;

    private bool isCurrentProtectionActive = true;

    private Pie currentPie = null;
    private SpriteRenderer spriteRend = null;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = IsProtected ? ProtectionSprite : NormalSprite;
    }

    public void SetCurrentPie(Pie newPie)
    {
        currentPie = newPie;
    }

    public bool HasPie()
    {
        return currentPie != null;
    }

    public bool TryStealPie()
    {
        if (IsProtected && isCurrentProtectionActive)
        {
            BreakProtection();
            return false;
        }
        else
        {
            StealPie();
            return true;
        }
    }

    public void BreakProtection()
    {
        isCurrentProtectionActive = false;
        spriteRend.sprite = NormalSprite;
    }

    public void RepairProtection()
    {
        isCurrentProtectionActive = true;
        spriteRend.sprite = ProtectionSprite;
    }

    private void StealPie()
    {
        Destroy(currentPie.gameObject);
    }
}
