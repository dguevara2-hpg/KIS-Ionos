using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetEd.CoreAdmin.DemoAppDotNet6.Models;

public partial class KisCoreContext : DbContext
{
    public KisCoreContext()
    {
    }

    public KisCoreContext(DbContextOptions<KisCoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<FeedbackType> FeedbackTypes { get; set; }

    public virtual DbSet<HistoryDocumentClick> HistoryDocumentClicks { get; set; }

    public virtual DbSet<HistoryFilter> HistoryFilters { get; set; }

    public virtual DbSet<HistorySearch> HistorySearches { get; set; }

    public virtual DbSet<HuddleUser> HuddleUsers { get; set; }

    public virtual DbSet<KisUser> KisUsers { get; set; }

    public virtual DbSet<PhysicianAdvisor> PhysicianAdvisors { get; set; }

    public virtual DbSet<ProviderIdn> ProviderIdns { get; set; }

    public virtual DbSet<RequestActivity> RequestActivities { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Analytics_Filter_Click> Analytics_Filter_Clicks { get; set; }

    public virtual DbSet<Analytics_Filter_Click_By_IDN> Analytics_Filter_Clicks_By_IDN { get; set; }

    public virtual DbSet<Analytics_Filter_Click_By_User> Analytics_Filter_Clicks_By_User { get; set; }

    public virtual DbSet<Analytics_Search_Term> Analytics_Search_Terms { get; set; }

    public virtual DbSet<Analytics_Search_Term_By_IDN> Analytics_Search_Terms_By_IDN { get; set; }

    public virtual DbSet<Analytics_Search_Term_By_User> Analytics_Search_Terms_By_User { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=VM0DDA2B6\\SQLEXPRESS19;Database=KIS_Core;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Category)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.ContractNumber)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Department)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.DocumentTitle)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("ID");
            entity.Property(e => e.ImportedDate).HasColumnType("datetime");
            entity.Property(e => e.InsightCategory)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.ItemType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Link)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.Path)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.Provisioning)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.Specialty)
                .HasMaxLength(800)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("documentId");
            entity.Property(e => e.Feedback1)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("feedback");
            entity.Property(e => e.FeedbackTypeId).HasColumnName("FeedbackTypeID");
            entity.Property(e => e.Selection)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("selection");
            entity.Property(e => e.SessionCount).HasColumnName("sessionCount");
            entity.Property(e => e.SessionId)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("sessionID");
            entity.Property(e => e.Tstamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("tstamp");
            entity.Property(e => e.Userid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userid");

            entity.HasOne(d => d.FeedbackType).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.FeedbackTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_FeedbackType");
        });

        modelBuilder.Entity<FeedbackType>(entity =>
        {
            entity.ToTable("FeedbackType");

            entity.Property(e => e.FeedbackTypeId).HasColumnName("FeedbackTypeID");
            entity.Property(e => e.FeedbackTypeDescription)
                .HasMaxLength(800)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistoryDocumentClick>(entity =>
        {
            entity.ToTable("History_DocumentClick");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DocumentId)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValueSql("((0))")
                .HasColumnName("documentId");
            entity.Property(e => e.Page)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("page");
            entity.Property(e => e.SessionCount).HasColumnName("sessionCount");
            entity.Property(e => e.SessionId)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("sessionID");
            entity.Property(e => e.Tstamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("tstamp");
            entity.Property(e => e.Userid)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("userid");
            entity.Property(e => e.Value)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<HistoryFilter>(entity =>
        {
            entity.ToTable("History_Filter");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Page)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("page");
            entity.Property(e => e.SessionCount).HasColumnName("sessionCount");
            entity.Property(e => e.SessionId)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("sessionID");
            entity.Property(e => e.Tstamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("tstamp");
            entity.Property(e => e.Userid)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("userid");
            entity.Property(e => e.Value)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<HistorySearch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Search");

            entity.ToTable("History_Search");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Page)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("page");
            entity.Property(e => e.SessionCount).HasColumnName("sessionCount");
            entity.Property(e => e.SessionId)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("sessionID");
            entity.Property(e => e.Tstamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("tstamp");
            entity.Property(e => e.Userid)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("userid");
            entity.Property(e => e.Value)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<HuddleUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Huddle_Users");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("First name");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("ID");
            entity.Property(e => e.Idn)
                .HasMaxLength(255)
                .HasColumnName("IDN");
            entity.Property(e => e.JobTitle).HasMaxLength(255);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("Last name");
            entity.Property(e => e.SsoIdentifier)
                .HasMaxLength(255)
                .HasColumnName("SSO identifier");
        });

        modelBuilder.Entity<KisUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("KIS_Users");

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(50)
                .HasColumnName("approvedBy");
            entity.Property(e => e.ApprovedDate)
                .HasMaxLength(1)
                .HasColumnName("approvedDate");
            entity.Property(e => e.DevelopmentAccess).HasColumnName("Development_Access");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .HasColumnName("emailAddress");
            entity.Property(e => e.FacilityId).HasColumnName("facilityID");
            entity.Property(e => e.FacilityIdn)
                .HasMaxLength(100)
                .HasColumnName("facilityIDN");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.Guid)
                .HasMaxLength(50)
                .HasColumnName("guid");
            entity.Property(e => e.HuddleId).HasColumnName("huddleID");
            entity.Property(e => e.IsApproved).HasColumnName("isApproved");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .HasColumnName("jobTitle");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(1)
                .HasColumnName("password");
            entity.Property(e => e.ProductionAccess).HasColumnName("Production_Access");
            entity.Property(e => e.RequestStatusId).HasColumnName("RequestStatusID");
            entity.Property(e => e.RequestedDate).HasColumnName("requestedDate");
            entity.Property(e => e.ResetPasswordKey)
                .HasMaxLength(1)
                .HasColumnName("resetPasswordKey");
            entity.Property(e => e.ResetPasswordTstamp)
                .HasMaxLength(1)
                .HasColumnName("resetPasswordTstamp");
            entity.Property(e => e.RoleId).HasColumnName("roleID");
            entity.Property(e => e.RoleType).HasColumnName("Role_Type");
            entity.Property(e => e.StagingAccess).HasColumnName("Staging_Access");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<PhysicianAdvisor>(entity =>
        {
            entity.ToTable("PhysicianAdvisor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BillingAddress).IsUnicode(false);
            entity.Property(e => e.Biography).IsUnicode(false);
            entity.Property(e => e.BoardCertifications).IsUnicode(false);
            entity.Property(e => e.Credentials).IsUnicode(false);
            entity.Property(e => e.Cv)
                .IsUnicode(false)
                .HasColumnName("CV");
            entity.Property(e => e.FacilityType).IsUnicode(false);
            entity.Property(e => e.Fellowships).IsUnicode(false);
            entity.Property(e => e.Headshot)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HealthSystem).IsUnicode(false);
            entity.Property(e => e.HospitalAffiliations).IsUnicode(false);
            entity.Property(e => e.HourlyRate)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.InternalTags).IsUnicode(false);
            entity.Property(e => e.MedicalSchool).IsUnicode(false);
            entity.Property(e => e.Npi)
                .IsUnicode(false)
                .HasColumnName("NPI");
            entity.Property(e => e.PhysicianFirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhysicianLastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryEmail)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Residency).IsUnicode(false);
            entity.Property(e => e.SecondaryEmail)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.SpeakerRating).IsUnicode(false);
            entity.Property(e => e.Specialty).IsUnicode(false);
            entity.Property(e => e.Subspecialty).IsUnicode(false);
            entity.Property(e => e.TravelRate)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.VendorNumber).IsUnicode(false);
        });

        modelBuilder.Entity<ProviderIdn>(entity =>
        {
            entity.ToTable("ProviderIDNS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdnName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("idn_name");
        });

        modelBuilder.Entity<RequestActivity>(entity =>
        {
            entity.HasKey(e => e.ActivityId);

            entity.ToTable("RequestActivity");

            entity.Property(e => e.ActivityId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("activityID");
            entity.Property(e => e.EmailType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("emailType");
            entity.Property(e => e.FacilityId).HasColumnName("facilityID");
            entity.Property(e => e.InternalNote)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("internalNote");
            entity.Property(e => e.RequestStatusId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("requestStatusID");
            entity.Property(e => e.RoleId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("roleID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.UGuid)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("uGUID");
            entity.Property(e => e.UserGuid)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("userGUID");

            entity.HasOne(d => d.Facility).WithMany(p => p.RequestActivities)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("FK_RequestActivity_ProviderIDNS");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.RequestActivities)
                .HasForeignKey(d => d.RequestStatusId)
                .HasConstraintName("FK_RequestActivity_RequestStatus");

            entity.HasOne(d => d.Role).WithMany(p => p.RequestActivities)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RequestActivity_UserRoles");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.RStatusId);

            entity.ToTable("RequestStatus");

            entity.Property(e => e.RStatusId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("rStatusId");
            entity.Property(e => e.StatusName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => new { e.Guid, e.Username });

            entity.Property(e => e.Guid)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("guid");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("approvedBy");
            entity.Property(e => e.ApprovedDate)
                .HasColumnType("datetime")
                .HasColumnName("approvedDate");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("emailAddress");
            entity.Property(e => e.FacilityId).HasColumnName("facilityID");
            entity.Property(e => e.FacilityIdn)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("facilityIDN");
            entity.Property(e => e.FirstName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.HuddleId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("huddleID");
            entity.Property(e => e.IsApproved)
                .HasComputedColumnSql("(case when [RequestStatusID]=(2) then (1) else (0) end)", false)
                .HasColumnName("isApproved");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("jobTitle");
            entity.Property(e => e.LastName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RequestStatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("RequestStatusID");
            entity.Property(e => e.RequestedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("requestedDate");
            entity.Property(e => e.ResetPasswordKey)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("resetPasswordKey");
            entity.Property(e => e.ResetPasswordTstamp)
                .HasColumnType("datetime")
                .HasColumnName("resetPasswordTstamp");
            entity.Property(e => e.RoleId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("roleID");

            entity.HasOne(d => d.Facility).WithMany(p => p.Users)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("FK_Users_ProviderIDNS");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.Users)
                .HasForeignKey(d => d.RequestStatusId)
                .HasConstraintName("FK_Users_RequestStatus");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_UserRoles");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("roleId");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Role)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        modelBuilder.Entity<Analytics_Filter_Click>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Filter)
                .HasColumnName("Filter");
            entity.Property(e => e.Clicks)
                .HasColumnType("int")
                .HasColumnName("Clicks");
        });

        modelBuilder.Entity<Analytics_Filter_Click_By_IDN>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Filter)
                .HasColumnName("Filter");
            entity.Property(e => e.HealthSystem)
                .HasColumnName("HealthSystem");
            entity.Property(e => e.Clicks)
                .HasColumnType("int")
                .HasColumnName("Clicks");
        });

        modelBuilder.Entity<Analytics_Filter_Click_By_User>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Filter)
                .HasColumnName("Filter");
            entity.Property(e => e.HealthSystem)
                .HasColumnName("HealthSystem");
            entity.Property(e => e.LastName)
                .HasColumnName("LastName");
            entity.Property(e => e.FirstName)
                .HasColumnName("FirstName");
            entity.Property(e => e.RecentVisit)
                .HasColumnType("date")
                .HasColumnName("RecentVisit");
            entity.Property(e => e.Clicks)
                .HasColumnType("int")
                .HasColumnName("Clicks");
        });

        modelBuilder.Entity<Analytics_Search_Term>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Term)
                .HasColumnName("Term");
            entity.Property(e => e.Searches)
                .HasColumnType("int")
                .HasColumnName("Searches");
        });

        modelBuilder.Entity<Analytics_Search_Term_By_IDN>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Term)
                .HasColumnName("Term");
            entity.Property(e => e.HealthSystem)
                .HasColumnName("HealthSystem");
            entity.Property(e => e.Searches)
                .HasColumnType("int")
                .HasColumnName("Searches");
        });

        modelBuilder.Entity<Analytics_Search_Term_By_User>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Term)
                .HasColumnName("Term");
            entity.Property(e => e.HealthSystem)
                .HasColumnName("HealthSystem");
            entity.Property(e => e.LastName)
                .HasColumnName("LastName");
            entity.Property(e => e.FirstName)
                .HasColumnName("FirstName");
            entity.Property(e => e.RecentVisit)
                .HasColumnType("date")
                .HasColumnName("RecentVisit");
            entity.Property(e => e.Searches)
                .HasColumnType("int")
                .HasColumnName("Searches");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
