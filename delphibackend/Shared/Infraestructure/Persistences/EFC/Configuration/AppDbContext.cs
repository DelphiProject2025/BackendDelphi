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
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOption { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SessionRecording> SessionRecording { get; set; }
        public DbSet<SharedFile> SharedFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de la entidad AuthUser
            builder.Entity<AuthUser>().HasKey(u => u.Id);
            builder.Entity<AuthUser>().Property(u => u.Id).IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<AuthUser>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_AuthUser_Email");
            builder.Entity<AuthUser>().Property(u => u.PasswordHash).IsRequired();
            
            // Configuración de la entidad Host
            builder.Entity<Host>(host =>
            {
                host.HasKey(h => h.Id);
                host.Property(h => h.Id).IsRequired().ValueGeneratedOnAdd();
                host.HasOne(h => h.AuthUser)
                    .WithOne(a => a.Host)
                    .HasForeignKey<Host>(h => h.AuthUserId)
                    .HasPrincipalKey<AuthUser>(a => a.Id);
            });

         // Configuración de la entidad Participant
            builder.Entity<Participant>(participant =>
            {
                participant.HasKey(p => p.Id);
                participant.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                participant.HasOne(p => p.AuthUser)
                    .WithOne(a => a.Participant)
                    .HasForeignKey<Participant>(p => p.AuthUserId)
                    .HasPrincipalKey<AuthUser>(a => a.Id);
            });

    // Configuración de la entidad Room
            builder.Entity<Room>(room =>
            {
                room.ToTable("Rooms");
                room.HasKey(r => r.Id);
                room.Property(r => r.Id).IsRequired();
                room.Property(r => r.RoomName).IsRequired().HasMaxLength(100);
                room.Property(r => r.Password).IsRequired(false);
                room.Property(r => r.HostId).IsRequired();

                room.HasOne(r => r.Host)
                    .WithMany()
                    .HasForeignKey(r => r.HostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Room_Host");

                room.HasMany(r => r.Participants)
                    .WithMany(p => p.Rooms)
                    .UsingEntity(j => j.ToTable("RoomParticipants"));

                room.HasOne(r => r.SharedFile)
                    .WithOne()
                    .HasForeignKey<Room>(r => r.SharedFileId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_SharedFile");

                room.HasMany(r => r.Questions)
                    .WithOne(q => q.Room)
                    .HasForeignKey(q => q.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_Questions");

                room.HasMany(r => r.Polls)
                    .WithOne(p => p.Room)
                    .HasForeignKey(p => p.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_Polls");

                room.HasOne(r => r.SessionRecording)
                    .WithOne(sr => sr.Room)
                    .HasForeignKey<SessionRecording>(sr => sr.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_SessionRecording");
                
                room.HasOne(r => r.Chat)
                    .WithOne(c => c.Room) // Define la propiedad inversa en Chat
                    .HasForeignKey<Room>(r => r.ChatId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Room_Chat");
            });
            
            // Configuración de la entidad Chat
            builder.Entity<Chat>(chat =>
            {
                chat.ToTable("Chats");
                chat.HasKey(c => c.Id);
                chat.Property(c => c.Id).IsRequired();

                // Relación uno a uno con Room
                chat.HasOne(c => c.Room)
                    .WithOne(r => r.Chat)
                    .HasForeignKey<Chat>(c => c.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Chat_Room");
            });
            
            // Configuración de la entidad Question
            builder.Entity<Question>(question =>
            {
                question.ToTable("Questions");
                question.HasKey(q => q.Id);
                question.Property(q => q.Text).IsRequired();
                question.Property(q => q.Answer).IsRequired(false);
                question.Property(q => q.Likes).IsRequired();

                question.HasOne(q => q.Room)
                    .WithMany(r => r.Questions)
                    .HasForeignKey(q => q.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Question_Room");

                question.HasOne(q => q.participant)
                    .WithMany()
                    .HasForeignKey(q => q.ParticipantId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Question_Participant");
            });

            // Configuración de la entidad Poll
            builder.Entity<Poll>(poll =>
            {
                poll.ToTable("Polls");
                poll.HasKey(p => p.Id);
                poll.Property(p => p.Question).IsRequired();
                poll.Property(p => p.IsActive).IsRequired();
                poll.Property(p => p.CreatedAt).IsRequired();

                poll.HasOne(p => p.Room)
                    .WithMany(r => r.Polls)
                    .HasForeignKey(p => p.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Poll_Room");

                poll.HasOne(p => p.Host)
                    .WithMany()
                    .HasForeignKey(p => p.HostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Poll_Host");
            });

            // Configuración de la entidad PollOption
            builder.Entity<PollOption>(pollOption =>
            {
                pollOption.ToTable("PollOptions");
                pollOption.HasKey(po => po.Id);
                pollOption.Property(po => po.OptionText).IsRequired();
                pollOption.Property(po => po.Votes).IsRequired();

                pollOption.HasOne(po => po.Poll)
                    .WithMany(p => p.Options)
                    .HasForeignKey(po => po.PollId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PollOption_Poll");
            });

            // Configuración de la entidad SessionRecording
            builder.Entity<SessionRecording>(recording =>
            {
                recording.ToTable("SessionRecordings");
                recording.HasKey(sr => sr.Id);
                recording.Property(sr => sr.RecordingUrl).IsRequired().HasMaxLength(500);
                recording.Property(sr => sr.StartDateTime).IsRequired();
                recording.Property(sr => sr.EndDateTime).IsRequired(false);
                recording.Property(sr => sr.FileSize).IsRequired();

                recording.HasOne(sr => sr.Room)
                    .WithOne(r => r.SessionRecording)
                    .HasForeignKey<SessionRecording>(sr => sr.RoomId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SessionRecording_Room");
            });

            // Configuración de la entidad SharedFile
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
