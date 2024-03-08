using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_Gob_Tracker.Models;

public partial class GobTrackerContext : DbContext
{
    public GobTrackerContext()
    {
    }

    public GobTrackerContext(DbContextOptions<GobTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllRawStat> AllRawStats { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerTeam> PlayerTeams { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScoringStat> ScoringStats { get; set; }

    public virtual DbSet<SeasonStat> SeasonStats { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<StatType> StatTypes { get; set; }

    public virtual DbSet<StatValsPerGame> StatValsPerGames { get; set; }

    public virtual DbSet<StatsPlayer> StatsPlayers { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamRoster> TeamRosters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllRawStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AllRawStats");

            entity.Property(e => e.Abrv)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Fname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("FName");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Lname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("LName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlayerTeamId).HasColumnName("PlayerTeamID");
            entity.Property(e => e.StatTypeId).HasColumnName("StatTypeID");
            entity.Property(e => e.StatValue).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Game");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateTimeId).HasColumnName("DateTimeID");
            entity.Property(e => e.Team1Id).HasColumnName("Team1ID");
            entity.Property(e => e.Team2Id).HasColumnName("Team2ID");

            /*
            entity.HasOne(d => d.Team1).WithMany(p => p.GameTeam1s)
                .HasForeignKey(d => d.Team1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team1ID");

            entity.HasOne(d => d.Team2).WithMany(p => p.GameTeam2s)
                .HasForeignKey(d => d.Team2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team2ID");
            */
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Fname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("LName");
        });

        modelBuilder.Entity<PlayerTeam>(entity =>
        {
            entity.ToTable("PlayerTeam");

            entity.HasIndex(e => new { e.PlayerId, e.TeamId }, "IX_PlayerTeam").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            /*
            entity.HasOne(d => d.Player).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerTeam_Player");

            entity.HasOne(d => d.Team).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerTeam_Team");
            */
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Schedule");

            entity.Property(e => e.AwayTeam)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateTimeId).HasColumnName("DateTimeID");
            entity.Property(e => e.HomeTeam)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<ScoringStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ScoringStats");

            entity.Property(e => e.GameID).HasColumnName("GameID");
            entity.Property(e => e.TeamID).HasColumnName("TeamID");
            entity.Property(e => e.TotalPtsMade).HasColumnType("decimal(38, 4)");
        });

        modelBuilder.Entity<SeasonStat>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("SeasonStats");

            entity.Property(e => e.Fname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("FName");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Lname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("LName");
            entity.Property(e => e.Total2PtsMade).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.Total3PtsMade).HasColumnType("decimal(38, 4)");
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.ToTable("Stat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.PlayerTeamId).HasColumnName("PlayerTeamID");
            entity.Property(e => e.StatTypeId).HasColumnName("StatTypeID");
            entity.Property(e => e.StatValue).HasColumnType("decimal(8, 4)");

            /*
            entity.HasOne(d => d.Game).WithMany(p => p.Stats)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stat_Game");

            entity.HasOne(d => d.PlayerTeam).WithMany(p => p.Stats)
                .HasForeignKey(d => d.PlayerTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stat_PlayerTeam");

            entity.HasOne(d => d.StatType).WithMany(p => p.Stats)
                .HasForeignKey(d => d.StatTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stat_Stat Type");
            */
        });


        modelBuilder.Entity<StatType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Stat Type");

            entity.ToTable("StatType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Abrv)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatValsPerGame>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("StatValsPerGame");

            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.StatName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatValue).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
        });

        modelBuilder.Entity<StatsPlayer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("StatsPlayer");

            entity.Property(e => e.Abrv)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Fname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("FName");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.Lname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("LName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.StatValue).HasColumnType("decimal(8, 4)");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TeamRoster>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Team Roster");

            entity.Property(e => e.Fname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("LName");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.PlayerTeamID).HasColumnName("PlayerTeamID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
