using delphibackend.Delphi.Domain.Model.Aggregates;
using delphibackend.Delphi.Domain.Model.Entities;
using delphibackend.IAM.Domain.Model.Aggregates;
using delphibackend.User.Domain.Model.Entities;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using Host = delphibackend.User.Domain.Model.Entities.Host;

namespace delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

     
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            builder.EnableSensitiveDataLogging();
            builder.AddCreatedUpdatedInterceptor();
            //builder.UseSnakeCaseNamingConvention();

            base.OnConfiguring(builder);
        }
       
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Host> Host { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOption { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SessionRecording> SessionRecording { get; set; }
        public DbSet<SharedFile> SharedFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // IAM Bounded Context
            builder.Entity<AuthUser>().HasKey(u => u.Id);
            builder.Entity<AuthUser>().Property(u => u.Id).IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<AuthUser>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_AuthUser_Email");
            builder.Entity<AuthUser>().Property(u => u.PasswordHash).IsRequired();

            // USER Bounded Context
            builder.Entity<Host>().HasKey(h => h.Id);
            builder.Entity<Host>().Property(h => h.Id).IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Host>()
                .HasOne(h => h.AuthUser)
                .WithOne(a => a.Host)
                .HasForeignKey<Host>(h => h.AuthUserId)
                .HasPrincipalKey<IAM.Domain.Model.Aggregates.AuthUser>(a => a.Id);
            
            builder.Entity<Participant>().HasKey(p => p.Id);
            builder.Entity<Participant>().Property(p => p.Id).IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<Participant>()
                .HasOne(p => p.AuthUser)
                .WithOne(a => a.Participant)
                .HasForeignKey<Participant>(p => p.AuthUserId)
                .HasPrincipalKey<IAM.Domain.Model.Aggregates.AuthUser>(a => a.Id);
            
            // ROOM 
            builder.Entity<Room>(room =>
            {
                room.ToTable("Rooms");
                room.HasKey(r => r.Id);
                room.Property(r => r.Id).IsRequired();
                room.Property(r => r.RoomName).IsRequired().HasMaxLength(100);
                room.Property(r => r.Password).IsRequired(false);
                room.Property(r => r.HostId).IsRequired();

                // Relación con Host
                room.HasOne(r => r.Host)
                    .WithMany()
                    .HasForeignKey(r => r.HostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Room_Host");
                
                room.HasMany(r => r.Participants)
                    .WithMany(p => p.Rooms) // Necesitas agregar esta propiedad en Participant
                    .UsingEntity(j => j.ToTable("RoomParticipants")); // Tabla intermedia

                // Relación con SharedFile (uno a uno)
                room.HasOne(r => r.SharedFile)
                    .WithOne()
                    .HasForeignKey<Room>(r => r.SharedFileId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_SharedFile");

                // Relación con Questions (uno a muchos)
                room.HasMany(r => r.Questions)
                    .WithOne(q => q.Room)
                    .HasForeignKey(q => q.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_Questions");

                // Relación con Polls (uno a muchos)
                room.HasMany(r => r.Polls)
                    .WithOne(p => p.Room)
                    .HasForeignKey(p => p.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_Polls");

                // Relación con SessionRecording (uno a uno)
                room.HasOne(r => r.SessionRecording)
                    .WithOne(sr => sr.Room)
                    .HasForeignKey<SessionRecording>(sr => sr.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_SessionRecording");

            });
            
            // **Configuración de Question**
            builder.Entity<Question>(question =>
            {
                question.ToTable("Questions");
                question.HasKey(q => q.Id);
                question.Property(q => q.Text).IsRequired();
                question.Property(q => q.Answer).IsRequired(false);
                question.Property(q => q.Likes).IsRequired();

                // Relación con Room
                question.HasOne(q => q.Room)
                    .WithMany(r => r.Questions)
                    .HasForeignKey(q => q.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Question_Room");

                // Relación con Participant
                question.HasOne(q => q.participant)
                    .WithMany()
                    .HasForeignKey(q => q.ParticipantId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Question_Participant");
            });
            
            // **Configuración de Poll**
            builder.Entity<Poll>(poll =>
            {
                poll.ToTable("Polls");
                poll.HasKey(p => p.Id);
                poll.Property(p => p.Question).IsRequired();
                poll.Property(p => p.IsActive).IsRequired();
                poll.Property(p => p.CreatedAt).IsRequired();

                // Relación con Room
                poll.HasOne(p => p.Room)
                    .WithMany(r => r.Polls)
                    .HasForeignKey(p => p.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Poll_Room");

                // Relación con Host
                poll.HasOne(p => p.Host)
                    .WithMany()
                    .HasForeignKey(p => p.HostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Poll_Host");
            });

            // **Configuración de PollOption**
            builder.Entity<PollOption>(pollOption =>
            {
                pollOption.ToTable("PollOptions");
                pollOption.HasKey(po => po.Id);
                pollOption.Property(po => po.OptionText).IsRequired();
                pollOption.Property(po => po.Votes).IsRequired();

                // Relación con Poll
                pollOption.HasOne(po => po.Poll)
                    .WithMany(p => p.Options)
                    .HasForeignKey(po => po.PollId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PollOption_Poll");
            });
            
            // **Configuración de SessionRecording**
            builder.Entity<SessionRecording>(recording =>
            {
                recording.ToTable("SessionRecordings");
                recording.HasKey(sr => sr.Id);

                recording.Property(sr => sr.RecordingUrl)
                    .IsRequired()
                    .HasMaxLength(500); // Puedes ajustar la longitud máxima según sea necesario

                recording.Property(sr => sr.StartDateTime)
                    .IsRequired();

                recording.Property(sr => sr.EndDateTime)
                    .IsRequired(false);

                recording.Property(sr => sr.FileSize)
                    .IsRequired();

                // Relación uno a uno con Room
                recording.HasOne(sr => sr.Room)
                    .WithOne(r => r.SessionRecording)
                    .HasForeignKey<SessionRecording>(sr => sr.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SessionRecording_Room");
            });
            
            // **Configuración de SharedFile**
            builder.Entity<SharedFile>(sharedFile =>
            {
                sharedFile.ToTable("SharedFiles");
                sharedFile.HasKey(sf => sf.Id);
                sharedFile.Property(sf => sf.Id).IsRequired();
                sharedFile.Property(sf => sf.Content).IsRequired(false);
                
            });

    }
        
        
    }
}
