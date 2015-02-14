using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

/*
 * Notice, at the moment sounds do not work well with Monogame and require
 * a content pipeline project in order to use them. It will be a while before
 * Monogame fixes this issue
 */

namespace Component_Based_Base_Game.Structure
{
    /// <summary>
    /// IDentifier for sets of assets to be loaded all at once
    /// </summary>
    public enum LoadSets
    {
        All
    }

    /// <summary>
    /// Names of individual assets in the asset library. This will likely be
    /// quite long.
    /// </summary>
    public enum AssetNames
    {
        Player,
        Blop_Sound,
    }

    /// <summary>
    /// Defines the properties of each texture asset that should be tracked
    /// </summary>
    public struct TextureProperties
    {
        public AssetNames Name;
        public String Path;
        public Texture2D Texture;
        public List<LoadSets> LoadSets;

        public TextureProperties(AssetNames name, String path)
        {
            this.Name = name;
            this.Path = path;
            this.Texture = null;
            this.LoadSets = new List<LoadSets>();
        }
    }

    /// <summary>
    /// Defines the properties of each sound asset that should be tracked
    /// </summary>
    public struct SoundProperties
    {
        public AssetNames Name;
        public String Path;
        public SoundEffect Sound;
        public List<LoadSets> LoadSets;

        public SoundProperties(AssetNames name, String path)
        {
            this.Name = name;
            this.Path = path;
            this.Sound = null;
            this.LoadSets = new List<LoadSets>();
        }
    }
    /// <summary>
    /// Manages the assets of the game
    /// </summary>
    public static class AssetManager
    {
        /// <summary>
        /// Dictionary of texture assets that links asset names with their properties. Similar to how components work.
        /// </summary>
        public static Dictionary<AssetNames, TextureProperties> TextureAssets = new Dictionary<AssetNames, TextureProperties>();

        /// <summary>
        /// Dictionary of sound assets that links asset names with their properties. Similar to how components work.
        /// </summary>
        public static Dictionary<AssetNames, SoundProperties> SoundAssets = new Dictionary<AssetNames, SoundProperties>();

        /// <summary>
        /// Tracks whether the asset dictionary has been initialized
        /// </summary>
        private static bool isInitialized = false;

        /// <summary>
        /// Graphics Device to use for loading textures
        /// </summary>
        private static GraphicsDevice _graphics;

        /// <summary>
        /// Adds all necessary assets to the asset dictionary so that they can be loaded and unloaded later
        /// </summary>
        public static void Initialize(GraphicsDevice graphics)
        {
            _graphics = graphics;

            TextureAssets.Add(AssetNames.Player, new TextureProperties(AssetNames.Player, @"Content/Main_Character.png"));

            SoundAssets.Add(AssetNames.Blop_Sound, new SoundProperties(AssetNames.Blop_Sound, @"Content/Blop.wav"));
            isInitialized = true;
        }

        /// <summary>
        /// Loads a single texture asset
        /// </summary>
        /// <param name="name">Name of the asset to load</param>
        /// <returns>The Texture that was loaded</returns>
        public static Texture2D LoadTexture(AssetNames name)
        {
            if (!isInitialized) throw new NullReferenceException("Asset Manager must be initialized before it can be used");
            if (!TextureAssets.ContainsKey(name)) return null;

            TextureProperties textureProperties = TextureAssets[name];

            if (textureProperties.Texture == null)
            {
                Stream stream = TitleContainer.OpenStream(textureProperties.Path);
                textureProperties.Texture = Texture2D.FromStream(_graphics, stream);
            }

            return textureProperties.Texture;
        }

        /// <summary>
        /// Loads a single sound effect asset
        /// </summary>
        /// <param name="name">Name of the asset to load</param>
        /// <returns>The Sound Effect that was loaded</returns>
        public static SoundEffect LoadSound(AssetNames name)
        {
            if (!isInitialized) throw new NullReferenceException("Asset Manager must be initialized before it can be used");
            if (!SoundAssets.ContainsKey(name)) return null;

            SoundProperties soundProperties = SoundAssets[name];

            if (soundProperties.Sound == null)
            {
                Stream stream = TitleContainer.OpenStream(soundProperties.Path);
                soundProperties.Sound = SoundEffect.FromStream(stream);
            }

            return soundProperties.Sound;
        }

        /// <summary>
        /// Unloads a single texture asset
        /// </summary>
        /// <param name="name">Name of texture asset to unload</param>
        public static void UnloadTexture(AssetNames name)
        {
            if (!isInitialized) throw new NullReferenceException("Asset Manager must be initialized before it can be used");
            if (!TextureAssets.ContainsKey(name)) return;

            TextureProperties textureProperties = TextureAssets[name];

            if(textureProperties.Texture != null)
            {
                textureProperties.Texture.Dispose();
                textureProperties.Texture = null;
            }
        }

        /// <summary>
        /// Unloads a single sound asset
        /// </summary>
        /// <param name="name">Name of sound asset to unload</param>
        public static void UnloadSound(AssetNames name)
        {
            if (!isInitialized) throw new NullReferenceException("Asset Manager must be initialized before it can be used");
            if (!SoundAssets.ContainsKey(name)) return;

            SoundProperties soundProperties = SoundAssets[name];

            if (soundProperties.Sound != null)
            {
                soundProperties.Sound.Dispose();
                soundProperties.Sound = null;
            }
        }

        /// <summary>
        /// Loads all assets in an assetSet
        /// </summary>
        /// <param name="loadSet">LoadSet to load in</param>
        public static void Load_LoadSet(LoadSets loadSet)
        {
            if (!isInitialized) throw new NullReferenceException("Asset Manager must be initialized before it can be used");
            List<KeyValuePair<AssetNames, TextureProperties>> textures = TextureAssets.Where(x => x.Value.LoadSets.Contains(loadSet)).ToList();
            List<KeyValuePair<AssetNames, SoundProperties>> sounds = SoundAssets.Where(x => x.Value.LoadSets.Contains(loadSet)).ToList();

            foreach(KeyValuePair<AssetNames, TextureProperties> kvp in textures)
            {
                LoadTexture(kvp.Key);
            }

            foreach(KeyValuePair<AssetNames, SoundProperties> kvp in sounds)
            {
                LoadSound(kvp.Key);
            }
        }

        /// <summary>
        /// Unloads all assets in an assetSet
        /// </summary>
        /// <param name="loadSet">LoadSet to unload</param>
        public static void Unload_LoadSet(LoadSets loadSet)
        {
            if (!isInitialized) throw new NullReferenceException("Asset Manager must be initialized before it can be used");
            List<KeyValuePair<AssetNames, TextureProperties>> textures = TextureAssets.Where(x => x.Value.LoadSets.Contains(loadSet)).ToList();
            List<KeyValuePair<AssetNames, SoundProperties>> sounds = SoundAssets.Where(x => x.Value.LoadSets.Contains(loadSet)).ToList();
            
            foreach (KeyValuePair<AssetNames, TextureProperties> kvp in textures)
            {
                UnloadTexture(kvp.Key);
            }

            foreach (KeyValuePair<AssetNames, SoundProperties> kvp in sounds)
            {
                UnloadSound(kvp.Key);
            }
        }

    }
}
