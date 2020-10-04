using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PieSpot : MonoBehaviour
{
    public bool IsProtected = false;
    public Button RepairButton = null;
    public Button ReplacePieButton = null;

    private bool isCurrentProtectionActive = true;
    private SpriteMask brokenGlassMask;

    private Pie currentPie = null;
    //private SpriteRenderer spriteRend = null;

    private void Awake()
    {
        brokenGlassMask = GetComponentInChildren<SpriteMask>();
        if (brokenGlassMask != null)
        {
            brokenGlassMask.enabled = false;
        }
        if (RepairButton != null)
        {
            RepairButton.gameObject.SetActive(false);
        }
        ReplacePieButton.gameObject.SetActive(false);
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
            RemovePie();
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

    public void RemovePie()
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
        else if (currentPie == null)
        {
            ReplacePieButton.gameObject.SetActive(true);
        }
    }

    public void HideButtons()
    {
        if (IsProtected)
        {
            RepairButton.gameObject.SetActive(false);
        }
        ReplacePieButton.gameObject.SetActive(false);
    }

    public void Repair()
    {
        if (GameManager.Instance.GetMoneyManager().TryRepair())
        { 
            RepairProtection();
            RepairButton.gameObject.SetActive(false);
            if (currentPie == null)
            {
                ReplacePieButton.gameObject.SetActive(true);
            }
        }
    }

    public void SetIsRepairButtonInteractable(bool state)
    {
        if (IsProtected)
        {
            RepairButton.interactable = state;
        }
    }

    public void SetIsReplacePieButtonInteractable(bool state)
    {
        ReplacePieButton.interactable = state;
    }

    public void InitPie(Pie piePrefab)
    {
        Pie newPie = Instantiate(piePrefab, transform.position, Quaternion.identity);
        currentPie = newPie;
    }

    public void ReplacePie(Pie piePrefab)
    {
        if (GameManager.Instance.GetMoneyManager().TryReplacePie())
        {
            Pie newPie = Instantiate(piePrefab, transform.position, Quaternion.identity);
            currentPie = newPie;
            ReplacePieButton.gameObject.SetActive(false);
        }
    }
}
