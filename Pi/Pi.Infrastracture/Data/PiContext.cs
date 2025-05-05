using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pi.Domain.Entities;
using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Pi.Infrastracture.Data
{
    public class PiContext : IdentityDbContext<Users, IdentityRole<long>, long>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PiContext(DbContextOptions<PiContext> options) : base(options)
        {
        }

        public PiContext(DbContextOptions<PiContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }




        //DbSet cho cacs entity (Domain entities)
        public DbSet<Modules> Modules { get; set; }
        public DbSet<Operations> Operations { get; set; }
        public DbSet<RoleOperations> RoleOperations { get; set; }
        public DbSet<UserOperations> UserOperations { get; set; }
        public DbSet<UserRoleAssignments> UserRoleAssignments { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }


        private void UpdateAuditFields()
        {
            var userName = GetCurentUserName();
            var userId = GetCurrentUserId();

            var entries = ChangeTracker.Entries()
            .Where(e => e.Entity != null &&
                e.Entity.GetType().GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntity<>)) &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

            var now = DateTime.Now;

            foreach (var entry in entries)
            {
                var entity = (IEntity<long>)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreateDate = now;
                    entity.CreateById = userId;
                    entity.CreatedBy = userName;
                }
                entity.UpdateDate = now;
                entity.UpdatedBy = userName;
                entity.UpdateById = userId;
            }
        }

        private string GetCurentUserName()
        {
            if (_httpContextAccessor.HttpContext?.User != null && _httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated != true)
            {
                return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "";
            }
            return string.Empty;
        }
        private long GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext?.User != null)
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                if (userId != null) return long.TryParse(userId.Value, out var id) ? id : 0;
            }
            return 0;

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Đổi tên các bảng cho các entity Identity
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<long>>().ToTable("Roles");
            //modelBuilder.Entity<IdentityUserRole<long>>().ToTable("UserRoleAssignments");
            //modelBuilder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");



            //Cấu hình các quan hệ giữa các bảng

            modelBuilder.Entity<Modules>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Operations>(x =>
            {
                x.HasKey(x => x.Id);

                x.HasOne(x => x.Modules)
                .WithMany(x => x.Operations)
                .HasForeignKey(x => x.ModuleId);
            });

            modelBuilder.Entity<RoleOperations>(x =>
            {
                x.HasKey(x => new { x.Id, x.RoleId, x.OperationId }); //Chỉ định khoá chính

                x.HasOne(x => x.Roles) //Mối quan hệ với Role
                .WithMany(x => x.RoleOperations) //Một Role có thể có nhiều RoleOperations
                .HasForeignKey(x => x.RoleId); //Định nghĩa khoá ngoại

                x.HasOne(x => x.Operations) //Mỗi quan hệ với Operation
                .WithMany(x => x.RoleOperations) //Một Operation có thể có nhiều RoleOperations
                .HasForeignKey(x => x.OperationId);
            });



            //Cấu hình các quy tắc bắt buộc
            //Cấu hình giá trị mặc định
            //Cấu hình giới hạn giá trị cho số


        }
    }
}
