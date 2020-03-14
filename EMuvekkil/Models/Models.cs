using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace EMuvekkil.Models
{

    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }

    public class Dava
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Avukat")]
        public string AvukatId { get; set; }

        [ForeignKey("Muvekkil")]
        public string MuvekkilId { get; set; }

        [ForeignKey("DavaState")]
        public int DavaStateId { get; set; }

        public DavaState DavaState { get; set; }

        public ApplicationUser Avukat { get; set; }
        public ApplicationUser Muvekkil { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Masraf> Masrafs { get; set; }
        //public virtual ICollection<Event> Events { get; set; }
    }

    public class Message
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        [ForeignKey("Dava")]
        public int DavaId { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public bool IsActive { get; set; }

        public Dava Dava { get; set; }
        public ApplicationUser Owner { get; set; }

    }

    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Muvekkils { get; set; }
    }

    public class Masraf
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        [ForeignKey("Dava")]
        public int DavaId { get; set; }

        public Dava Dava { get; set; }
        public ApplicationUser Owner { get; set; }
    }

    public class Document
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Dava")]
        public int DavaId { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }
        public Dava Dava { get; set; }

    }


    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime RememberDate { get; set; }
        public IList<EventUsers> EventUsers { get; set; }
        public string ReminderJobId { get; set; }
    }

    public class Notification
    {   
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class EventUsers
    {
        [Key]
        public int EventId { get; set; }
        public Event Event { get; set; }
        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        public string Id { get; set; }
        public string IdentityNumber { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public int DavaDependencies { get; set; }
    }

    public class DavaViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string AvukatName { get; set; }
        public string MuvekkilName { get; set; }
        public string AvukatId { get; set; }
        public string MuvekkilId { get; set; }
        public string DavaStateText { get; set; }
        public int DavaStateId { get; set; }
    }

    public enum Role
    {
        admin, avukat, muvekkil
    }

    public class UserInfoModel
    {
        public string Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public Role Role { get; set; }
    }

    public class UserViewModel
    {
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public string Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int DavaDependencies { get; set; }
    }

    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string DavaName { get; set; }
        public int DavaId { get; set; }
        public DateTime Date { get; set; }
        public string OwnerName { get; set; }
        public string OwnerUserName { get; set; }
        public bool IsActive { get; set; }
    }

    public class NewDavaModel
    {
        public string Name { get; set; }
        public string MuvekkilId { get; set; }
        public string AvukatId { get; set; }

    }

    public class MasrafViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string OwnerUserName { get; set; }
        public string OwnerName { get; set; }
        public int DavaId { get; set; }
        public DateTime Date { get; set; }
    }

    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string OwnerUserName { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string DavaName { get; set; }
        public int DavaId { get; set; }
        public IFormFile File { get; set; }
        public bool IsActive { get; set; }


    }

    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Dependecies { get; set; }
    }

    public class DavaState
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class DavaStateViewModel
    {

        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class ReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AvukatId { get; set; }
        public string MuvekkilId { get; set; }
        public int DavaId { get; set; }
        public bool DateDisabled { get; set; }
    }

    public class ReportListModel
    {
        public IList<Masraf> Masrafs { get; set; }
        public IList<Document> Documents { get; set; }
        public IList<Message> Messages { get; set; }

    }

    public class ReportListViewModel
    {
        public IList<MasrafViewModel> Masrafs { get; set; }
        public IList<DocumentViewModel> Documents { get; set; }
        public IList<MessageViewModel> Messages { get; set; }

    }

    public class EventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime RememberDate { get; set; }
        public string ReminderJobId { get; set; }
        public IList<UserViewModel> Users { get; set; }

    }

    public class NotificationViewModel{
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        
    }
    

}