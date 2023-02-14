using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RoboschienWeb.Models.Entities
{
    public partial class SickLeaveContext : DbContext
    {
        public SickLeaveContext()
        {

        }

        public SickLeaveContext(DbContextOptions<SickLeaveContext> options)
            : base(options)
        {
            //var objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            // objectContext.CommandTimeout = 120;

        }

        public virtual DbSet<EmailInformation> EmailInformations { get; set; }
        public virtual DbSet<EmployeeLeaveInformation> EmployeeLeaveInformations { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<JobConfiguration> JobConfigurations { get; set; }
        public virtual DbSet<JobExecutionDetail> JobExecutionDetails { get; set; }
        public virtual DbSet<JobStatus> JobStatus { get; set; }
        public virtual DbSet<EmailConfiguration> EmailConfigurations { get; set; }
        public virtual DbSet<GlobalApplicationVersionDetails> GlobalApplicationVersionDetails { get; set; }
        public virtual DbSet<ReferenceNumberDetails> ReferenceNumberDetails { get; set; }
        public virtual DbSet<GlobalReferenceNumberDetails> GlobalReferenceNumberDetails { get; set; }
        public virtual DbSet<IllnessTypeDetails> IllnessTypeDetails { get; set; }
        public virtual DbSet<RequestTypeDetails> RequestTypeDetails { get; set; }
        public virtual DbSet<KindofIllnessDetails> KindofIllnessDetails { get; set; }
        public virtual DbSet<EmployeeLeaveInformation_FR> EmployeeLeaveInformation_FR { get; set; }
        public virtual DbSet<EmployeeLeaveInformation_PT> EmployeeLeaveInformation_PT { get; set; }
        public virtual DbSet<EmployeeLeaveInformation_ES> EmployeeLeaveInformation_ES { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailInformation>(entity =>
            {
                entity.ToTable("EmailInformation");

                entity.HasIndex(e => e.LeaveId)
                    .HasName("IX_FK_EmailInformation_EmployeeLeaveInformation");

                entity.Property(e => e.BccMailId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Leave)
                    .WithMany(p => p.EmailInformations)
                    .HasForeignKey(d => d.LeaveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailInformation_EmployeeLeaveInformation");
            });

            modelBuilder.Entity<EmailConfiguration>(entity =>
            {
                entity.ToTable("EmailConfiguration");

                entity.HasIndex(e => e.id)
                 .HasName("IX_FK_EmailConfigurations");


                entity.Property(e => e.EmailType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EmailSubject)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);
                entity.Property(e => e.EmailFrom)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
                entity.Property(e => e.EmailTo)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false);

                entity.Property(e => e.MailContentPart1)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode(false);

                entity.Property(e => e.MailContentPart2)
             .IsRequired()
             .HasMaxLength(500)
             .IsUnicode(false);
                entity.Property(e => e.MailContentPart3)
             .IsRequired()
             .HasMaxLength(500)
             .IsUnicode(false);


                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");


            });

            modelBuilder.Entity<EmployeeLeaveInformation>(entity =>
            {
                entity.ToTable("EmployeeLeaveInformation");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(300);

                entity.Property(e => e.EndDate).HasMaxLength(200);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.EmployeeId)
                   .IsRequired()
                   .HasMaxLength(100);

                entity.Property(e => e.Locale)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.ReferenceNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RequestSource)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasMaxLength(200);

            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLog");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobConfiguration>(entity =>
            {
                entity.ToTable("JobConfiguration");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.JobDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.JobFrequency)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<JobExecutionDetail>(entity =>
            {
                entity.HasIndex(e => e.JobId)
                    .HasName("IX_FK_JobExecutionDetails_JobConfiguration");

                entity.HasIndex(e => e.StatusId)
                    .HasName("IX_FK_JobExecutionDetails_JobStatus");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobExecutionDetailJobs)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobExecutionDetails_JobConfiguration");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.JobExecutionDetailStatus)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_JobExecutionDetails_JobStatus");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.JobExecutionDetails)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__JobExecut__Statu__108B795B");
            });

            modelBuilder.Entity<JobStatus>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.JobStatus1)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.StatusDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            //modelBuilder.Entity<ReferenceNumberDetails>(entity =>
            //{
            //    entity.Property(e => e.Id).HasColumnType("int");
            //    entity.Property(e => e.ReferenceNumber).HasColumnType("bigint").IsConcurrencyToken();
            //});

        }
    }
}
