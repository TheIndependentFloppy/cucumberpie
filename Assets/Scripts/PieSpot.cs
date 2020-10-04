using UnityEngine;
using UnityEngine.UI;

public class PieSpot : MonoBehaviour
{
    public bool IsProtected = false;
    public Button RepairButton = null;

    private bool isCurrentProtectionActive = true;
    private SpriteMask brokenGlassMask;

    private Pie currentPie = null;
    //private SpriteRenderer spriteRend = null;

    private void Awake()
    {
        if (transform.childCount > 0)
        {
            brokenGlassMask = transform.GetChild(0).GetComponent<SpriteMask>();
            brokenGlassMask.enabled = false;
            RepairButton.gameObject.SetActive(false);
        }
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

    private void RepairProtection()
    {
        isCurrentProtectionActive = true;
        brokenGlassMask.enabled = false;
    }

    private void StealPie()
    {
        Destroy(currentPie.gameObject);
    }

    public void ShowRepairButton()
    {
        if (IsProtected && !isCurrentProtectionActive)
        {
            RepairButton.gameObject.SetActive(true);
            SetIsRepairButtonInteractable(GameManager.Instance.GetMoneyManager().GetCurrentMoney() >= GameManager.Instance.GetMoneyManager().PriceRepair);
        }
    }

    public void HideRepairButton()
    {
        if (IsProtected)
        {
            RepairButton.gameObject.SetActive(false);
        }
    }

    public void Repair()
    {
        if (GameManager.Instance.GetMoneyManager().TryRepair())
        { 
            RepairProtection();
            HideRepairButton();
        }
    }

    public void SetIsRepairButtonInteractable(bool state)
    {
        if (IsProtected)
        {
            RepairButton.interactable = state;
        }
    }
}
