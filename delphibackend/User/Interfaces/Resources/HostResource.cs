namespace delphibackend.User.Interfaces.Resources
{
    public class HostResource
    {
        public Guid Id { get; set; } // Identificador único del Host
        public Guid AuthUserId { get; set; } // Identificador del usuario que actúa como Host
        public DateTime CreatedAt { get; set; } // Fecha de creación
        public bool IsActive { get; set; } // Estado activo/inactivo
    }
}