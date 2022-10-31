using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Transform glowingBlockPrefab;

    public void StartLightSequence()
    {
        Level_Manager levelManager = GetLevelManager();
        levelManager.CountLitBlocks();
        Light();
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

    private void Light()
    {
        Transform glowingBlock = Instantiate(glowingBlockPrefab, transform.parent.transform);
        glowingBlock.position = transform.position;
    }

    private Level_Manager GetLevelManager()
    {
        Level_Manager levelManager = GameObject.Find("Level_Manager").GetComponent<Level_Manager>();
        return levelManager;
    }
}