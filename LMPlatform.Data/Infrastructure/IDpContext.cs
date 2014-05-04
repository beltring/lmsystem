﻿using System.Data.Entity;
using LMPlatform.Models;
using LMPlatform.Models.DP;

namespace LMPlatform.Data.Infrastructure
{
    public interface IDpContext
    {
        int SaveChanges();

        DbSet<Role> Roles { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Student> Students { get; set; }

        DbSet<Group> Groups { get; set; }

        DbSet<Lecturer> Lecturers { get; set; }

        DbSet<AssignedDiplomProject> AssignedDiplomProjects { get; set; }

        DbSet<DiplomPercentagesGraph> DiplomPercentagesGraphs { get; set; }

        DbSet<DiplomPercentagesResult> DiplomPercentagesResults { get; set; }

        DbSet<DiplomProjectConsultationDate> DiplomProjectConsultationDates { get; set; }

        DbSet<DiplomProjectConsultationMark> DiplomProjectConsultationMarks { get; set; }

        DbSet<DiplomProjectGroup> DiplomProjectGroups { get; set; }

        DbSet<DiplomProject> DiplomProjects { get; set; }
    }
}