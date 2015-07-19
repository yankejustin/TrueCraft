using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TrueCraft.API;
using TrueCraft.Client.Rendering;
using System.Linq;
using System.ComponentModel;
using TrueCraft.Core.Networking.Packets;
using TrueCraft.API.World;
using System.Collections.Concurrent;
using TrueCraft.Client.Input;
using TrueCraft.Core;
using TrueCraft.Client.Graphics;
using OpenTK.Graphics.OpenGL;
using TrueCraft.Client.Events;

namespace TrueCraft.Client
{
    public class TrueCraftGame : Game
    {
        private MultiplayerClient Client { get; set; }
        private IPEndPoint EndPoint { get; set; }
        private ChunkRenderer ChunkConverter { get; set; }
        private DateTime NextPhysicsUpdate { get; set; }
        private List<Mesh> ChunkMeshes { get; set; }
        public ConcurrentBag<Action> PendingMainThreadActions { get; set; }
        private ConcurrentBag<Mesh> IncomingChunks { get; set; }
        private TextureMapper TextureMapper { get; set; }

        public TrueCraftGame(MultiplayerClient client, IPEndPoint endPoint)
            : base(UserSettings.Local.WindowResolution.Width,
                UserSettings.Local.WindowResolution.Height,
                "TrueCraft", UserSettings.Local.IsFullscreen)
        {
            Client = client;
            EndPoint = endPoint;
            NextPhysicsUpdate = DateTime.MinValue;
            ChunkMeshes = new List<Mesh>();
            IncomingChunks = new ConcurrentBag<Mesh>();
            PendingMainThreadActions = new ConcurrentBag<Action>();
        }

        protected override void OnLoad(object sender, EventArgs e)
        {
            // Initializes various runtime components.
            base.OnLoad(sender, e);

            Initialize();
            LoadContent();
        }

        private void Initialize()
        {
            ChunkConverter = new ChunkRenderer(this, Client.World.World.BlockRepository);
            Client.ChunkLoaded += Client_ChunkLoaded;
            //Client.ChunkModified += (sender, e) => ChunkConverter.Enqueue(e.Chunk, true);
            ChunkConverter.MeshCompleted += ChunkConverter_MeshGenerated;
            ChunkConverter.Start();
            Client.PropertyChanged += HandleClientPropertyChanged;
            Client.Connect(EndPoint);
            LoadContent();
            GL.ClearColor(System.Drawing.Color.CornflowerBlue);
        }

        void Client_ChunkLoaded(object sender, ChunkEventArgs e)
        {
            ChunkConverter.Enqueue(e.Chunk);
        }

        void ChunkConverter_MeshGenerated(object sender, RendererEventArgs<ReadOnlyChunk> e)
        {
            PendingMainThreadActions.Add(() =>
            {
                IncomingChunks.Add(e.Result);
            });
        }

        void HandleClientPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Position":
                    //UpdateCamera();
                    var sorter = new ChunkRenderer.ChunkSorter(new Coordinates3D(
                        (int)Client.Position.X, 0, (int)Client.Position.Z));
                    PendingMainThreadActions.Add(() => ChunkMeshes.Sort(sorter));
                    break;
            }
        }

        private void LoadContent()
        {
            // Ensure we have default textures loaded.
            TextureMapper.LoadDefaults();

            // Load any custom textures if needed.
            TextureMapper = new TextureMapper();
            if (UserSettings.Local.SelectedTexturePack != TexturePack.Default.Name)
                TextureMapper.AddTexturePack(TexturePack.FromArchive(Path.Combine(TexturePack.TexturePackPath, UserSettings.Local.SelectedTexturePack)));
        }

        protected override void OnUpdate(object sender, FrameEventArgs e)
        {
            Mesh mesh;
            while (IncomingChunks.TryTake(out mesh))
                ChunkMeshes.Add(mesh);

            Action action;
            if (PendingMainThreadActions.TryTake(out action))
                action();

            if (NextPhysicsUpdate < DateTime.Now && Client.LoggedIn)
            {
                IChunk chunk;
                var adjusted = Client.World.World.FindBlockPosition(new Coordinates3D((int)Client.Position.X, 0, (int)Client.Position.Z), out chunk);
                if (chunk != null)
                {
                    if (chunk.GetHeight((byte)adjusted.X, (byte)adjusted.Z) != 0)
                        Client.Physics.Update();
                }
                // NOTE: This is to make the vanilla server send us chunk packets
                // We should eventually make some means of detecing that we're on a vanilla server to enable this
                // It's a waste of bandwidth to do it on a TrueCraft server
                Client.QueuePacket(new PlayerGroundedPacket { OnGround = true });
                Client.QueuePacket(new PlayerPositionAndLookPacket(Client.Position.X, Client.Position.Y,
                    Client.Position.Y + MultiplayerClient.Height, Client.Position.Z, Client.Yaw, Client.Pitch, false));
                NextPhysicsUpdate = DateTime.Now.AddMilliseconds(1000 / 20);
            }
        }

        protected override void OnRender(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (var chunk in ChunkMeshes)
                chunk.Draw();
        }

        protected override void OnUnload(object sender, EventArgs e)
        {
            Client.ChunkLoaded -= Client_ChunkLoaded;
            ChunkConverter.Dispose();

            // Cleans up various runtime components (and should be run last).
            base.OnUnload(sender, e);
        }
    }
}
