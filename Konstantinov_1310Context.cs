using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WpfApp2
{
    public partial class Konstantinov_1310Context : DbContext
    {
        public Konstantinov_1310Context()
        {
        }

        public Konstantinov_1310Context(DbContextOptions<Konstantinov_1310Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelComment> HotelComments { get; set; }
        public virtual DbSet<HotelImage> HotelImages { get; set; }
        public virtual DbSet<HotelOfTour> HotelOfTours { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<TypeOfTour> TypeOfTours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=310-03\\SQLEXPRESS; Database=Konstantinov_13-10; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("Country");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountCode).HasColumnName("count_code");

                entity.Property(e => e.CountOfStars).HasColumnName("count_of_stars");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.HasOne(d => d.CountCodeNavigation)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.CountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotel_Country");
            });

            modelBuilder.Entity<HotelComment>(entity =>
            {
                entity.ToTable("Hotel_Comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("author");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.HotelId).HasColumnName("hotel_id");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("text");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelComments)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotel_Comment_Hotel");
            });

            modelBuilder.Entity<HotelImage>(entity =>
            {
                entity.ToTable("Hotel_Image");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HotelId).HasColumnName("hotel_id");

                entity.Property(e => e.ImageSource)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("image_source");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelImages)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotel_Image_Hotel");
            });

            modelBuilder.Entity<HotelOfTour>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Hotel_Of_Tour");

                entity.Property(e => e.HotelId).HasColumnName("hotel_id");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Hotel)
                    .WithMany()
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotel_Of_Tour_Hotel");

                entity.HasOne(d => d.Tour)
                    .WithMany()
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotel_Of_Tour_Tour");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .IsRequired(true)
                    .HasColumnName("description");

                entity.Property(e => e.ImagePreview)
                    .HasColumnType("varbinary(MAX)")
                    .IsRequired(true)
                    .HasColumnName("image_preview");

                entity.Property(e => e.IsActual).HasColumnName("is_actual");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.TicketCount).HasColumnName("ticket_count");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description").IsRequired(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TypeOfTour>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Type_Of_Tour");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Tour)
                    .WithMany()
                    .HasForeignKey(d => d.TourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Type_Of_Tour_Tour");

                entity.HasOne(d => d.Type)
                    .WithMany()
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Type_Of_Tour_Type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
