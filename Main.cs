using DarkieCustomTraits.Content;
using HarmonyLib;
using NeoModLoader.api;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DarkieCustomTraits;

public class DarkieTraitsMain : BasicMod<DarkieTraitsMain>, IReloadable
{
    internal static bool _reload_switch;

    /// <summary>
    ///     <para>
    ///         To test reloading function, you can modify traits in <see cref="DarkieTraits" /> or trait action in
    ///         <see cref="DarkieTraitActions" /> then click Reload button in mod list
    ///     </para>
    ///     <para>You can modify them both in once reloading.</para>
    /// </summary>
    // Let the method can be hotfixed when it is modified and after the mod is reloaded. You can add this attribute at runtime.
    [Hotfixable]
    public void Reload()
    {
        _reload_switch = !_reload_switch;
        DarkieTraits.Init();
    }

    protected override void OnModLoad()
    {
        LogInfo("Darkie Traits Starting Up And Is Running!");
        //Config.isEditor = true; //Only for debug purpose
        DarkieTraits.Init();
        DarkieItems.Init();
        DarkieEffects.Init();
        DarkieUnits.Init();
        DarkieStatusEffects.Init();
    }
}