using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour
{
    private float zCoord;
    private Vector3 offSet;
    public int damage { get; set; }

    [SerializeField]
    private GameObject meatballsEfectPrefab;

    private void Start()
    {
        DataManager.canMoveCamera = false;
    }

    private void OnDestroy()
    {
        if (DataManager.isNeedToDestroy)
        {
            GameObject meatballs = Instantiate(meatballsEfectPrefab);
            meatballs.transform.position = transform.position;

            float radiusHit = GetComponent<SphereCollider>().radius;

            Collider[] targets = Physics.OverlapSphere(transform.position, radiusHit, DataManager.ENEMY_LAYER_MASK);
            foreach (Collider target in targets)
            {
                target.GetComponent<Enemy>().TakeDamage(damage);
            }

            AbilitiesManager.GetAbility(Abilities.meatballsAbility).DeactivateAbility(Abilities.meatballsAbility);
        }

        Sprite sprite = AbilitiesManager.GetAbility(Abilities.meatballsAbility).artWork;
        UnityAction action = AbilitiesManager.GetAbility(Abilities.meatballsAbility).Use;
        AbilityDisplay.onChangeArtwork(Abilities.meatballsAbility, sprite, action);

        DataManager.canMoveCamera = true;
    }

    private void Update()
    {
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offSet = gameObject.transform.position - GetMouseWorldPos()*Time.deltaTime;

        if (Input.touchCount != 1)
        {
            return;
        }

        Touch touch = Input.touches[0];

        if (touch.phase == TouchPhase.Began)
        {
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offSet = gameObject.transform.position - GetMouseWorldPos() * Time.deltaTime;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            Vector3 newPos = GetMouseWorldPos() - offSet * Time.deltaTime;
            newPos.y = 0.5f;
            transform.position = newPos;
        }

        if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            if(IsPointerOverUIObject())
                DataManager.isNeedToDestroy = false;

            Destroy(gameObject);
        }
    }

    private static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Input.mousePosition;

        pos.z = zCoord;

        return Camera.main.ScreenToWorldPoint(pos);
    }

    private void OnMouseUp()
    {
       Destroy(gameObject);
    }
}
