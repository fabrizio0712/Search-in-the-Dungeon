using UnityEngine;

public class CompassUIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RectTransform compassIndicator;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = gameManager.CurrentActiveArtifact.transform.position - gameManager.Player.transform.position;
        dir.y = 0;
        dir.Normalize();
        float compassAngle = Vector3.SignedAngle(dir, gameManager.Player.BodyOrientation.forward,Vector3.up);
        compassIndicator.eulerAngles = new Vector3(0, 0, compassAngle - 90);
    }
}
