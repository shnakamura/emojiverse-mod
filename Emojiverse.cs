﻿using Emojiverse.IO.Readers;
using Emojiverse.IO.Sources;
using Microsoft.Xna.Framework;
using ReLogic.Content;
using ReLogic.Content.Readers;
using ReLogic.Content.Sources;
using ReLogic.Utilities;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Assets;

namespace Emojiverse;

public sealed class Emojiverse : Mod
{
    public static ResourcePackContentSource Source { get; private set; }
    public static new AssetRepository Assets { get; private set; }

    internal AssetRepository ModAssets => base.Assets;

    public override void Load() {
        var device = Main.instance.GraphicsDevice;
        var readers = new AssetReaderCollection();

        readers.RegisterReader(new PngReader(device), ".png");
        readers.RegisterReader(new RawImgReader(device), ".rawimg");
        readers.RegisterReader(new XnbReader(Main.instance.Services), ".xnb");
        readers.RegisterReader(new GifReader(device), ".gif");
        readers.RegisterReader(new JpgReader(device), ".jpg", ".jpeg", ".jpe");

        Source = new ResourcePackContentSource();
        Assets = new AssetRepository(
            readers,
            new IContentSource[] {
                Source
            }
        );
        
        UpdateSource(Main.AssetSourceController.ActiveResourcePackList);

        On_Main.DoUpdate += DoUpdateHook;
        
        Main.AssetSourceController.OnResourcePackChange += UpdateSource;
    }

    public override void Unload() {
        if (Assets != null) {
            var assets = Assets;
            Main.QueueMainThreadAction(() => {
                assets.TransferCompletedAssets();
                assets.Dispose();
            });
        }
        Assets = null;
        Source?.Clear();
        Source = null;
        On_Main.DoUpdate -= DoUpdateHook;
        Main.AssetSourceController.OnResourcePackChange -= UpdateSource; // does not unload automatically
    }

    private static void UpdateSource(ResourcePackList list) {
        Source.Update(list);
    }

    private static void DoUpdateHook(On_Main.orig_DoUpdate orig, Main self, ref GameTime gameTime) {
        orig(self, ref gameTime);

        Assets.TransferCompletedAssets();
    }
}