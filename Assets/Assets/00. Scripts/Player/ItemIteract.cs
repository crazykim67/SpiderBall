using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemInfo
{
    Magnet,
    Hook,
}

public class ItemIteract : MonoBehaviour
{
    [SerializeField]
    private ItemController itemController;
    private Rigidbody2D rg;
    private HookController hookController;

    private Vector2 velocity;

    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        hookController = GetComponent<HookController>();
    }

    private void Update()
    {
        velocity = rg.velocity;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.CompareTag("Booster"))
        {
            Destroy(coll.gameObject);
            rg.AddForce(velocity * 100f, ForceMode2D.Force);
        }
        else if (coll.transform.CompareTag("ArrowBooster"))
        {
            if (hookController.isAttach)
                hookController.UnGrappling();

            rg.velocity = Vector2.zero;
            Destroy(coll.gameObject);
            rg.AddForce((Vector2.left + Vector2.up).normalized * 1000f, ForceMode2D.Force);
        }

        if (coll.transform.CompareTag("Item"))
        {
            var _item = coll.GetComponent<FieldItem>();
            Destroy(coll.gameObject);

            switch (_item.info)
            {
                case ItemInfo.Magnet:
                    {
                        GameObject magnet = new GameObject("Magnet");
                        magnet.transform.parent = this.transform;

                        magnet.transform.localScale = new Vector3(1, 1, 1);
                        magnet.transform.localPosition = Vector3.zero;
                        magnet.transform.localRotation = Quaternion.identity;

                        magnet.AddComponent<Magnet>().controller = itemController;

                        itemController.OnAddItem(magnet.GetComponent<Item>());
                        break;
                    }
                case ItemInfo.Hook:
                    {
                        HookController controller = FindObjectOfType<HookController>();
                        if (controller == null)
                            return;

                        controller.hook.AddCount(1);
                        break;
                    }
            }

        }
    }
}
