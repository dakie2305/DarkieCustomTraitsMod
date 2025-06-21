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
    //internal static Transform? prefab_library;

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
        // Reload Example Trait. It's optional.
        DarkieTraits.Init();
    }


    /// <summary>
    ///     <para>
    ///         You can initialize you mod here, some methods called in order OnModLoad -> Awake -> OnEnable -> Start ->
    ///         Update -> Update -> ...
    ///     </para>
    /// </summary>
    protected override void OnModLoad()
    {
        LogInfo("Darkie Traits Starting Up And Is Running!");
        //Config.isEditor = true;
        DarkieTraits.Init();
        DarkieItems.Init();
        // Example of new tab and buttons.
        //ExampleGodPowers.init();
        // Example of adding items, item modifiers and item materials.
        //ExampleItems.Init();
        // Example of creating event handlers and add new world log message.
        // It implements two event handlers to handle Plot Start event and World Log Message format event to add new world log message type for tipping plot start.
        //ExampleEventHandlers.init();
    }
}