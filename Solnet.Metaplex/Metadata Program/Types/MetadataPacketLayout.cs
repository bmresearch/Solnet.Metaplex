



namespace Solnet.Metaplex.NFT.Library
{
    /// <summary>
    /// Create Instruction packet layout
    /// </summary>
    public static class MetadataPacketLayout
    {
        /// <summary>
        /// Discriminator Stream Position
        /// </summary>
        public const int MethodOffset = 0;
        /// <summary>
        /// Name Stream Position
        /// </summary>
        public const int nameOffset = 65;
        /// <summary>
        /// Symbol Stream Position
        /// </summary>
        public const int symbolOffset = 101;
        /// <summary>
        /// URL Stream Position
        /// </summary>
        public const int uriOffset = 115;
        /// <summary>
        /// FeeBasis Stream Position
        /// </summary>
        public const int feeBasisOffset = 319;
        /// <summary>
        /// Creator Stream Position
        /// </summary>
        public const int creatorSwitchOffset = 321;
        /// <summary>
        /// Creators Count Stream Position
        /// </summary>
        public const int creatorsCountOffset = 322;

        //Anything beyond this point is dynamically determined based on data provided
    }
}