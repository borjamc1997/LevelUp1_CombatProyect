using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{

    [SerializeField] Transform leftArmHint;
    [SerializeField] Transform leftArmTarget;
    [SerializeField] Transform rightArmHint;
    [SerializeField] Transform rightArmTarget;

    [SerializeField] Rig armsRig;

    [SerializeField] Transform weaponsParent;
    Weapon[] weapons;
    private int currentWeaponIndex = -1;

    [SerializeField] public UnityEvent<Weapon> onWeaponSelected;

    private void Awake()
    {
        weapons = weaponsParent.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons) { weapon.gameObject.SetActive(false); }
    }

    private void Start()
    {
        OnSelectWeapon0();
    }

    float oldAnalogValue = 0f;

    #region Input Listeners Methods
    private void OnShoot(InputValue analogValue)
    {
        if (currentWeaponIndex != -1 && weapons[currentWeaponIndex] is FireWeapon) 
        {
            float newAnalogValue = analogValue.Get<float>();
            if (weapons[currentWeaponIndex].CanUse())
            {
                if (oldAnalogValue <= 0f && newAnalogValue >= 0f) { weapons[currentWeaponIndex]?.Use(); }
            }
            else if (weapons[currentWeaponIndex].CanContinuousUse())
            {
                if (oldAnalogValue <= 0f && newAnalogValue >= 0f) { weapons[currentWeaponIndex]?.StartUsing(); }
                else if (oldAnalogValue > 0f && newAnalogValue <= 0f) { weapons[currentWeaponIndex]?.StopUsing(); }
            }
            oldAnalogValue = newAnalogValue;
        }
    }

    private void OnSelectWeapon0() { SelectWeapon(-1); }
    private void OnSelectWeapon1() { SelectWeapon(0); }
    private void OnSelectWeapon2() { SelectWeapon(1); }
    private void OnSelectWeapon3() { SelectWeapon(2); }
    private void OnSelectWeapon4() { SelectWeapon(3); }
    private void OnSelectWeapon5() { SelectWeapon(4); }
    private void OnSelectWeapon6() { SelectWeapon(5); }
    private void OnSelectWeapon7() { SelectWeapon(6); }
    private void OnSelectWeapon8() { SelectWeapon(7); }
    private void OnSelectWeapon9() { SelectWeapon(8); }

    private void OnChangeWeaponMouse(InputValue deltaValue)
    {
        int newWeaponIndex = currentWeaponIndex;
        Vector2 delta = deltaValue.Get<Vector2>();
        if (delta.y < 0)
        {
            newWeaponIndex--;
            if (newWeaponIndex < -1) { newWeaponIndex = weapons.Length - 1; }
        }
        else if(delta.y > 0) 
        {
            newWeaponIndex++;
            if (newWeaponIndex >= weapons.Length) { newWeaponIndex = - 1; }
        }
        SelectWeapon(newWeaponIndex);
    }

    private void OnChangeToNextWeaponJoystick()
    {
        int newWeaponIndex = currentWeaponIndex;
        newWeaponIndex++;
        if (newWeaponIndex >= weapons.Length) { newWeaponIndex = -1; }
        SelectWeapon(newWeaponIndex);
    }

    private void OnChangeToPreviousWeaponJoystick()
    {
        int newWeaponIndex = currentWeaponIndex;
        newWeaponIndex--;
        if (newWeaponIndex < -1) { newWeaponIndex = weapons.Length - 1; }
        SelectWeapon(newWeaponIndex);
    }
    #endregion

    private void SelectWeapon(int newWeaponIndex)
    {
        if (newWeaponIndex < weapons.Length)
        {
            if (currentWeaponIndex != -1)
            { weapons[currentWeaponIndex].gameObject.SetActive(false); }

            currentWeaponIndex = newWeaponIndex;

            if (currentWeaponIndex != -1)
            { 
                weapons[currentWeaponIndex].gameObject.SetActive(true);
                armsRig.weight = weapons[currentWeaponIndex].HasGrabPoints() ? 1f : 0f;
            }
            else
            { armsRig.weight = 0f; }
        }
        onWeaponSelected.Invoke(currentWeaponIndex == -1 ? null : weapons[currentWeaponIndex]);
    }

    private void LateUpdate()
    {
        if (currentWeaponIndex != -1)
        {
            Weapon actualWeapon = weapons[currentWeaponIndex];
            if (actualWeapon.HasGrabPoints())
            {
                UpdateHintAndTargetTransform(leftArmHint, actualWeapon.GetLeftArmHint());
                UpdateHintAndTargetTransform(leftArmTarget, actualWeapon.GetLeftArmTarget());
                UpdateHintAndTargetTransform(rightArmHint, actualWeapon.GetRightArmHint());
                UpdateHintAndTargetTransform(rightArmTarget, actualWeapon.GetRightArmTarget());
            }
        }
        
    }

    private void UpdateHintAndTargetTransform(Transform targetTransform, Transform newTransform)
    {
        targetTransform.transform.position = newTransform.position;
        targetTransform.transform.rotation = newTransform.rotation;
    }

}
