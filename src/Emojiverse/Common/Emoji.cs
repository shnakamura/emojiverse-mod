using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Emojiverse.Common;

public readonly struct Emoji(string name, string alias, string path, string identifier)
{
    /// <summary>
    ///     The name of this emoji.
    /// </summary>
    public readonly string Name = name;

    /// <summary>
    ///     The alias of this emoji.
    /// </summary>
    public readonly string Alias = alias;

    /// <summary>
    ///		The path to this emoji.
    /// </summary>
    public readonly string Path = path;

    /// <summary>
    ///     The unique identifier of this emoji.
    /// </summary>
    public readonly string Identifier = identifier;
}
