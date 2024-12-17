namespace delphibackend.User.Domain.Model.Entities;

public enum ParticipantRole
{
    Contributor,   // Usuario activo que puede interactuar
    Observer,      // Usuario que solo puede observar
    Moderator      // Usuario con permisos especiales
}