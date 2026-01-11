using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public GhostController[] ghosts;

    public void TriggerFrightened()
    {
        foreach (GhostController g in ghosts)
        {
            g.SetFrightened();
        }
    }
}
