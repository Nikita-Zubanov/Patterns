namespace Patterns.Visitor.Entities.AutoParts
{
    /// <summary>
    /// Прицеп.
    /// </summary>
    public class Trailer
    {
        /// <summary>
        /// Масса прицепа.
        /// </summary>
        public double WeightInKg { get; set; }

        /// <summary>
        /// Тип прицепа.
        /// </summary>
        public TrailerType TrailerType { get; set; }
    }
}