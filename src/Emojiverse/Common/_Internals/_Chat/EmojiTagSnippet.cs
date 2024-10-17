using Emojiverse.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI.Chat;

namespace Emojiverse.Common;

internal sealed class EmojiTagSnippet(Emoji emoji) : TextSnippet
{
    public readonly Emoji Emoji = emoji;

    public override void OnHover() {
        base.OnHover();

        Main.instance.MouseText(Emoji.Name.SurroundWith(':'));
    }

    public override bool UniqueDraw(
        bool justCheckingString,
        out Vector2 size,
        SpriteBatch spriteBatch,
        Vector2 position = new(),
        Color color = new(),
        float scale = 1
    ) {
        const int Size = 20;

        if (!justCheckingString && (color.R != 0 || color.G != 0 || color.B != 0)) {
            var texture = ModContent.Request<Texture2D>(Emoji.Path).Value;

            var frame = texture.Frame();
            var origin = frame.Size() / 2f;

            var multiplier = EmojiverseConfig.Instance.EmojiDrawingScale;

            var area = new Vector2(Size) * multiplier;
            var offset = area * (1f / Size) / 2f + area / 2f + new Vector2(0f, 4f);

            var rectangle = new Rectangle(
                (int)(position.X + offset.X),
                (int)(position.Y + offset.Y),
                (int)(Size * multiplier),
                (int)(Size * multiplier)
            );

            spriteBatch.Draw(texture, rectangle, frame, Color.White, 0f, origin, SpriteEffects.None, 0f);
        }

        size = new Vector2(Size);

        return true;
    }
}
