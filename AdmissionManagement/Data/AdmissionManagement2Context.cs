using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdmissionsManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AdmissionManagement.Models;

namespace AdmissionManagement.Data
{
    public class AdmissionManagement2Context : IdentityDbContext
    {
        public AdmissionManagement2Context (DbContextOptions<AdmissionManagement2Context> options)
            : base(options)
        {
        }

        public DbSet<AdmissionsManagement.Models.Cadidate> Cadidate { get; set; } = default!;
        public DbSet<AdmissionsManagement.Models.ApplicationUser> ApplicationUser { get; set; } = default!;
        public DbSet<AdmissionsManagement.Models.CorrectAnswer> CorrectAnswer { get; set; } = default!;

        public DbSet<AdmissionsManagement.Models.Course> Course { get; set; } = default!;

        public DbSet<AdmissionsManagement.Models.ExamQuestion> ExamQuestion { get; set; } = default!;

        public DbSet<AdmissionsManagement.Models.Option> Option { get; set; } = default!;

        public DbSet<AdmissionsManagement.Models.Question> Question { get; set; } = default!;

        public DbSet<AdmissionsManagement.Models.Exam> Exam { get; set; } = default!;

        public DbSet<AdmissionManagement.Models.SubjectCombination> SubjectCombination { get; set; } = default!;
        public DbSet<AdmissionManagement.Models.PaymentDetails> PaymentDetails { get; set; } = default!;
        public DbSet<AdmissionManagement.Models.Location> Location { get; set; } = default!;
        public DbSet<AdmissionManagement.Models.LocationCadidate> LocationCadidate { get; set; } = default!;



    }
}
