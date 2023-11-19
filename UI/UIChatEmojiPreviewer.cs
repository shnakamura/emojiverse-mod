﻿using System;
using System.Collections.Generic;
using System.Linq;
using Emojiverse.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace Emojiverse.UI;

public sealed class UIChatEmojiPreviewer : UIState
{
    public override void Update(GameTime gameTime) {
        if (!Main.drawingPlayerChat) {
            return;
        }

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        if (!Main.drawingPlayerChat) {
            return;
        }

        var elementHeight = 20;
        var maxElements = 20;
        
        var menuHeight = maxElements * elementHeight;
        var rectangle = new Rectangle(78, Main.screenHeight - menuHeight - 40, Main.screenWidth / 4, menuHeight);

        spriteBatch.Draw(TextureAssets.MagicPixel.Value, rectangle, Color.Black * 0.75f);

        base.Draw(spriteBatch);
    }
}

public sealed class UIChatEmojiPreviewerSystem : ModSystem
{
    public UIChatEmojiPreviewer State { get; private set; }
    public UserInterface UserInterface { get; private set; }

    public override void Load() {
        State = new UIChatEmojiPreviewer();
        State.Activate();

        UserInterface = new UserInterface();
        UserInterface.SetState(State);
    }

    public override void UpdateUI(GameTime gameTime) {
        UserInterface.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
        var layer = layers.FindIndex(x => x.Name == "Vanilla: Player Chat");

        if (layer == -1) {
            return;
        }

        layers.Insert(
            layer + 1,
            new LegacyGameInterfaceLayer(
                "Emojiverse:ChatEmojiPreviewer",
                delegate {
                    UserInterface.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI
            )
        );
    }
}
