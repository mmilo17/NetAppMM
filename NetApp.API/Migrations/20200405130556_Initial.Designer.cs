﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetApp.API.Data;

namespace NetApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200405130556_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("NetApp.API.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime?>("DateRead");

                    b.Property<bool>("IsRead");

                    b.Property<int>("MessageRecipientId");

                    b.Property<int>("MessageSenderId");

                    b.Property<DateTime>("MessageSent");

                    b.Property<bool>("RecipientDeleted");

                    b.Property<bool>("SenderDeleted");

                    b.HasKey("Id");

                    b.HasIndex("MessageRecipientId");

                    b.HasIndex("MessageSenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("NetApp.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<bool>("IsMain");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("NetApp.API.Models.Request", b =>
                {
                    b.Property<int>("SenderId");

                    b.Property<int>("ReceiverId");

                    b.HasKey("SenderId", "ReceiverId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("NetApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bio");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Gender");

                    b.Property<DateTime>("LastActive");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Surnname");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NetApp.API.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("NetApp.API.Models.Message", b =>
                {
                    b.HasOne("NetApp.API.Models.User", "MessageRecipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("MessageRecipientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NetApp.API.Models.User", "MessageSender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("MessageSenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("NetApp.API.Models.Photo", b =>
                {
                    b.HasOne("NetApp.API.Models.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NetApp.API.Models.Request", b =>
                {
                    b.HasOne("NetApp.API.Models.User", "Receiver")
                        .WithMany("Senders")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NetApp.API.Models.User", "Sender")
                        .WithMany("Recivers")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}