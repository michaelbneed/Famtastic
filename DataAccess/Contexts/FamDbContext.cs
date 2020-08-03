using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entity;
using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
	public class FamDbContext : IdentityDbContext<AspNetUser>
	{
		public FamDbContext(DbContextOptions<FamDbContext> options)
			: base(options)
		{

		}

		public virtual DbSet<Attachment> Attachment { get; set; }
		public virtual DbSet<Chat> Chat { get; set; }
		public virtual DbSet<ChatMessage> ChatMessage { get; set; }
		public virtual DbSet<ConversationFamily> ConversationFamily { get; set; }
		public virtual DbSet<ConversationFamilyMessage> ConversationFamilyMessage { get; set; }
		public virtual DbSet<Family> Family { get; set; }
		public virtual DbSet<FamilyCollection> FamilyCollection { get; set; }
		public virtual DbSet<FamilyCollectionDetail> FamilyCollectionDetail { get; set; }
		public virtual DbSet<FamilyPhotoGallery> FamilyPhotoGallery { get; set; }
		public virtual DbSet<Media> Media { get; set; }
		public virtual DbSet<Note> Note { get; set; }
		public virtual DbSet<NoteListItem> NoteListItem { get; set; }
		public virtual DbSet<Recipe> Recipe { get; set; }
		public virtual DbSet<UserProfile> UserProfile { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

			builder.Entity<Attachment>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.ChatMessageId).HasColumnName("ChatMessageID");

				entity.Property(e => e.FamilyMessageId).HasColumnName("FamilyMessageID");

				entity.Property(e => e.NoteId).HasColumnName("NoteID");

				entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
			});

			builder.Entity<Chat>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.OriginatingUserId)
					.HasColumnName("OriginatingUserID")
					.HasMaxLength(450);

				entity.Property(e => e.ReceivingUserId)
					.HasColumnName("ReceivingUserID")
					.HasMaxLength(450);

				entity.Property(e => e.Subject)
					.HasMaxLength(50)
					.IsUnicode(false);
			});

			builder.Entity<ChatMessage>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.ChatThreadId).HasColumnName("ChatThreadID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.MessageText).IsUnicode(false);

				entity.Property(e => e.UserId)
					.HasColumnName("UserID")
					.HasMaxLength(450);
			});

			builder.Entity<ConversationFamily>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

				entity.Property(e => e.Topic)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");

				entity.HasOne(d => d.Family)
					.WithMany(p => p.ConversationFamily)
					.HasForeignKey(d => d.FamilyId)
					.HasConstraintName("FK_ConversationFamily_Family");
			});

			builder.Entity<ConversationFamilyMessage>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.MessageText)
					.IsRequired()
					.IsUnicode(false);

				entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");

				entity.HasOne(d => d.Conversation)
					.WithMany(p => p.ConversationFamilyMessage)
					.HasForeignKey(d => d.ConversationId)
					.HasConstraintName("FK_ConversationFamilyMessage_ConversationFamily");
			});

			builder.Entity<Family>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.Description).IsUnicode(false);

				entity.Property(e => e.FamilyAdminUserProfileId).IsUnicode(false);

				entity.Property(e => e.Picture).HasColumnType("image");

				entity.Property(e => e.PictureContentType).IsUnicode(false);

				entity.Property(e => e.FamilyLastName)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.Title)
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.InvitationCode)
					.HasMaxLength(50)
					.IsUnicode(false);
			});

			builder.Entity<FamilyCollection>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

				entity.Property(e => e.Title)
					.IsRequired()
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.UserId)
					.HasColumnName("UserID")
					.HasMaxLength(450);
			});

			builder.Entity<FamilyCollectionDetail>(entity =>
			{
				entity.Property(e => e.Id)
					.HasColumnName("ID")
					.ValueGeneratedNever();

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.Description).IsUnicode(false);

				entity.Property(e => e.FamilyCollectionId).HasColumnName("FamilyCollectionID");

				entity.Property(e => e.Item)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.UserId)
					.HasColumnName("UserID")
					.HasMaxLength(450);
			});

			builder.Entity<FamilyPhotoGallery>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.Description).IsUnicode(false);

				entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

				entity.Property(e => e.Title)
					.HasMaxLength(50)
					.IsUnicode(false);
			});

			builder.Entity<Media>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.Description).IsUnicode(false);

				entity.Property(e => e.Image).HasColumnType("image");

				entity.Property(e => e.Title)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.ImageContentType).IsUnicode(false);

				entity.Property(e => e.VideoContentType).IsUnicode(false);

				entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");

				entity.Property(e => e.Video).HasColumnType("image");

				entity.HasOne(d => d.UserProfile)
					.WithMany(p => p.Media)
					.HasForeignKey(d => d.UserProfileId)
					.HasConstraintName("FK_Media_UserProfile");
			});

			builder.Entity<Note>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.DueDate).HasColumnType("datetime");

				entity.Property(e => e.Text).IsUnicode(false);

				entity.Property(e => e.Title)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");

				entity.HasOne(d => d.UserProfile)
					.WithMany(p => p.Note)
					.HasForeignKey(d => d.UserProfileId)
					.HasConstraintName("FK_Note_UserProfile");
			});

			builder.Entity<NoteListItem>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.CreatedBy).HasMaxLength(256);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.NoteId).HasColumnName("NoteID");

				entity.Property(e => e.NoteListItem1)
					.HasColumnName("NoteListItem")
					.HasMaxLength(50);

				entity.HasOne(d => d.Note)
					.WithMany(p => p.NoteListItem)
					.HasForeignKey(d => d.NoteId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_NoteListItem_Note");
			});

			builder.Entity<Recipe>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.Comments).IsUnicode(false);

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.Description)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

				entity.Property(e => e.Ingredients).IsUnicode(false);

				entity.Property(e => e.Instructions).IsUnicode(false);

				entity.Property(e => e.Name)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.UserProfileId).HasColumnName("UserProfileID");

				entity.HasOne(d => d.UserProfile)
					.WithMany(p => p.Recipe)
					.HasForeignKey(d => d.UserProfileId)
					.HasConstraintName("FK_Recipe_UserProfile");
			});

			builder.Entity<UserProfile>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.Email).HasMaxLength(256);

				entity.Property(e => e.FamilyId).HasColumnName("FamilyID");

				entity.Property(e => e.FirstName).HasMaxLength(256);

				entity.Property(e => e.LastName).HasMaxLength(256);

				entity.Property(e => e.Picture).HasColumnType("image");

				entity.Property(e => e.PictureContentType).IsUnicode(false);

				entity.Property(e => e.Profile).HasMaxLength(256);

				entity.Property(e => e.UserId)
					.IsRequired()
					.HasColumnName("UserID")
					.HasMaxLength(450);

				entity.HasOne(d => d.Family)
					.WithMany(p => p.UserProfile)
					.HasForeignKey(d => d.FamilyId)
					.HasConstraintName("FK_UserProfile_Family");

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.CreatedOn).HasColumnType("datetime");

				entity.Property(e => e.UpdatedBy)
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
			});


		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Data Source=SQL5045.site4now.net;Initial Catalog=DB_A45EDD_FamData;User Id=DB_A45EDD_FamData_admin;Password=G0ingStr0ng2019");
			}
		}
	}
}
