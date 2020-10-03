using UnityEngine;

public class PieSpot : MonoBehaviour
{
    public bool IsProtected = false;

    private bool isCurrentProtectionActive = true;
    private SpriteMask brokenGlassMask;

    private Pie currentPie = null;
    //private SpriteRenderer spriteRend = null;

    private void Awake()
    {
        brokenGlassMask = transform.GetChild(0).GetComponent<SpriteMask>();
        brokenGlassMask.enabled = false;
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
        brokenGlassMask.enabled = true;
    }

    public void RepairProtection()
    {
        isCurrentProtectionActive = true;
        brokenGlassMask.enabled = false;
    }

    private void StealPie()
    {
        Destroy(currentPie.gameObject);
    }
}
