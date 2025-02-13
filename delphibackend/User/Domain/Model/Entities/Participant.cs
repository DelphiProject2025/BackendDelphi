﻿    using delphibackend.IAM.Domain.Model.Aggregates;

    namespace delphibackend.User.Domain.Model.Entities;

    public class Participant
    {

        public Participant()
        {
            Role = ParticipantRole.Contributor;
            IsAnonymous = false;
            IsActive = true;
            JoinedAt = DateTime.UtcNow;
        }
        
        public Guid Id { get; set; } // Identificador único = Guid.NewGuid();
        public Guid AuthUserId { get; set; } // Identificador del Usuario

        public AuthUser? AuthUser { get; set; } // Nullable para evitar errores de referencia nula
        public string DisplayName => IsAnonymous || AuthUser == null ? "Anonymous" : AuthUser.Name;     
        public ParticipantRole Role { get; set; } // Rol dentro de la sesión
        public bool IsAnonymous { get; set; } // Si participa de forma anónima
        public bool IsActive { get; set; } // Estado del participante
        public DateTime JoinedAt { get; set; } // Fecha y hora de ingreso
        //hola soy erick


    }