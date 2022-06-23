using System.Runtime.Serialization;

namespace Patterns.CQRSEventSourcing.WritingShop.DataContracts
{
    /// <summary>
    /// Базовый класс сущности.
    /// </summary>
    [DataContract]
    public abstract class Entity
    {
        /// <summary>
        /// Id сущности.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
    }
}