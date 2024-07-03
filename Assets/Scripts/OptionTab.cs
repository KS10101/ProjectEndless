using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionTab : MonoBehaviour
{
    public bool IsCorrect = false;
    [HideInInspector] public OptionsContainer container;
    [SerializeField] private TextMeshProUGUI OptionText;

    public void Initialize(bool isCorrect, string answer)
    {
        OptionText.text = answer;
        this.IsCorrect = isCorrect;
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().sharedMaterial = mat;
    }

    public void SetAlpha(float alpha)
    {
        Color col = OptionText.color;
        col.a = alpha;
        OptionText.color = col;
    }

    public void ToggleCollider(bool state)
    {
        GetComponent<BoxCollider>().enabled = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (container.answered) return;

        if (other.GetComponent<Runner>() == null) return;

        if (IsCorrect)
        {
            container.OnRightOptionHit();
        }
        else
        {
            container.OnWrongOptionHit();
        }
    }
}
