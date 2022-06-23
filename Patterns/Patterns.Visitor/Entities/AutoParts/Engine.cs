namespace Patterns.Visitor.Entities.AutoParts
{
    /// <summary>
    /// Двигатель.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Объем в литрах.
        /// </summary>
        public double VolumeInLiters { get; set; }

        /// <summary>
        /// Наличие турбокомпрессора.
        /// </summary>
        public bool HasTurbo { get; set; }
    }
}