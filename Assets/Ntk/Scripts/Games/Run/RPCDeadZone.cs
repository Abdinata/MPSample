using Photon.Pun;
using UnityEngine;

public class RPCDeadZone : MonoBehaviour
{
    PhotonView photonView;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    [PunRPC]
    void MovePlayer(int countDead)
    {
        Vector3 pos = new Vector3(
            GameManager.Instance.RunnerSpawner.spawnLoc[countDead].position.x,
            GameManager.Instance.RunnerSpawner.spawnLoc[countDead].position.y,
            GameManager.Instance.RunnerSpawner.spawnLoc[countDead].position.z
            );

        transform.position = pos;
    }
}
