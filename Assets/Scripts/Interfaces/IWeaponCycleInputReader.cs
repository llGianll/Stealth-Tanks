using System;

public interface IWeaponCycleInputReader
{
    public int WeaponIndex { get; }
    public Action<IWeaponCycleInputReader> OnInputPressed { get; set; }
}
