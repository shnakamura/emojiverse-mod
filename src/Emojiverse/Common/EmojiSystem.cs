namespace Emojiverse.Common;

[Autoload(Side = ModSide.Client)]
public sealed class EmojiSystem : ModSystem
{
	public override void Load() {
		base.Load();
	}

	public override void Unload() {
		base.Unload();
	}

	public static bool TryGetEmoji(string alias, out Emoji emoji) {
		emoji = default;

		return true;
	}
}
