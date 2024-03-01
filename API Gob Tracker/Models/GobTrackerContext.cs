﻿using System;
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

    public DbSet<Game> Games { get; set; } = default!;

    public DbSet<Player> Players { get; set; } = default!;

    public DbSet<PlayerTeam> PlayerTeams { get; set; } = default!;

    public DbSet<Stat> Stats { get; set; } = default!;

    public DbSet<StatType> StatTypes { get; set; } = default!;

    public DbSet<Team> Teams { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Game");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateTimeId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DateTimeID");
            entity.Property(e => e.Team1Id)
                .HasColumnName("Team1ID");
            entity.Property(e => e.Team2Id)
                .HasColumnName("Team2ID");

            /*entity.HasOne(d => d.Team1).WithMany(p => p.GameTeam1s)
                .HasForeignKey(d => d.Team1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team1ID");

            entity.HasOne(d => d.Team2).WithMany(p => p.GameTeam2s)
                .HasForeignKey(d => d.Team2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team2ID");*/
        });

        modelBuilder.Entity<Player>(entity =>
        {
            /*entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");*/
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

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            /*entity.HasOne(d => d.Player).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerTeam_Player");

            entity.HasOne(d => d.Team).WithMany(p => p.PlayerTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerTeam_Team");*/
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.ToTable("Stat");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.PlayerTeamId).HasColumnName("PlayerTeamID");
            entity.Property(e => e.StatTypeId).HasColumnName("StatTypeID");

           /* entity.HasOne(d => d.Game).WithMany(p => p.Stats)
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
                .HasConstraintName("FK_Stat_Stat Type");*/
        });

        modelBuilder.Entity<StatType>(entity =>
        {
            entity.ToTable("Stat Type");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}