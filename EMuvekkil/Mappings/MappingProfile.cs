using System.Linq;
using AutoMapper;
using EMuvekkil.Models;

namespace EMuvekkil.Mappings
{

    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(mv => mv.Date, m => m.MapFrom(s => s.Date))
                .ForMember(mv => mv.DavaName, m => m.MapFrom(s => s.Dava.Name))
                .ForMember(mv => mv.DavaId, m => m.MapFrom(s => s.Dava.Id))
                .ForMember(mv => mv.Text, m => m.MapFrom(s => s.Text))
                .ForMember(mv => mv.OwnerName, m => m.MapFrom(s => s.Owner.NameSurname))
                .ForMember(mv => mv.OwnerUserName, m => m.MapFrom(s => s.Owner.UserName))
                .ForMember(mv => mv.Id, m => m.MapFrom(s => s.Id));
        }
    }

    public class DavaProfile : Profile
    {
        public DavaProfile()
        {
            CreateMap<Dava, DavaViewModel>()
            .ForMember(dv => dv.AvukatName, d => d.MapFrom(s => s.Avukat.NameSurname))
            .ForMember(dv => dv.Id, d => d.MapFrom(s => s.Id))
            .ForMember(dv => dv.MuvekkilName, d => d.MapFrom(s => s.Muvekkil.NameSurname))
            .ForMember(dv => dv.Name, d => d.MapFrom(s => s.Name))
            .ForMember(dv => dv.AvukatId, d => d.MapFrom(s => s.AvukatId))
            .ForMember(dv => dv.MuvekkilId, d => d.MapFrom(s => s.MuvekkilId))
            .ForMember(dv => dv.DavaStateId, d => d.MapFrom(s => s.DavaStateId))
            .ForMember(dv => dv.DavaStateText, d => d.MapFrom(s => s.DavaState.Text))
            ;
        }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>()
            .ForMember(uvm => uvm.Id, u => u.MapFrom(us => us.Id))
            .ForMember(uvm => uvm.Email, u => u.MapFrom(us => us.Email))
            .ForMember(uvm => uvm.NameSurname, u => u.MapFrom(us => us.NameSurname))
            .ForMember(uvm => uvm.IdentityNumber, u => u.MapFrom(us => us.IdentityNumber))
            .ForMember(uvm => uvm.CompanyId, u => u.MapFrom(us => us.CompanyId))
            .ForMember(uvm => uvm.CompanyName, u => u.MapFrom(us => us.Company.Name))
            ;
        }
    }

    public class MasrafProfile : Profile
    {
        public MasrafProfile()
        {
            CreateMap<Masraf, MasrafViewModel>()
            .ForMember(mvm => mvm.Id, m => m.MapFrom(s => s.Id))
            .ForMember(mvm => mvm.Date, m => m.MapFrom(s => s.Date))
            .ForMember(mvm => mvm.Amount, m => m.MapFrom(s => s.Amount))
            .ForMember(mvm => mvm.DavaId, m => m.MapFrom(s => s.Dava.Id))
            .ForMember(mvm => mvm.OwnerUserName, m => m.MapFrom(s => s.Owner.Email))
            .ForMember(mvm => mvm.OwnerName, m => m.MapFrom(s => s.Owner.NameSurname))
            .ForMember(mvm => mvm.Description, m => m.MapFrom(s => s.Description));

        }
    }


    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyViewModel>()
            .ForMember(cvm => cvm.Id, c => c.MapFrom(s => s.Id))
            .ForMember(cvm => cvm.Name, c => c.MapFrom(s => s.Name))
            .ForMember(cvm => cvm.Dependecies, c => c.MapFrom(s => s.Muvekkils.Count))
            ;

        }
    }

    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentViewModel>()
            .ForMember(dvm => dvm.Id, d => d.MapFrom(s => s.Id))
            .ForMember(dvm => dvm.Date, d => d.MapFrom(s => s.Date))
            .ForMember(dvm => dvm.DavaName, d => d.MapFrom(s => s.Dava.Name))
            .ForMember(dvm => dvm.FileName, d => d.MapFrom(s => s.FileName))
            .ForMember(dvm => dvm.OwnerName, d => d.MapFrom(s => s.Owner.NameSurname))
            .ForMember(dvm => dvm.OwnerUserName, d => d.MapFrom(s => s.Owner.UserName))
            .ForMember(dvm => dvm.Description, d => d.MapFrom(s => s.Description))
            ;
        }
    }

    public class DavaStateProfile : Profile
    {
        public DavaStateProfile()
        {
            CreateMap<DavaState, DavaStateViewModel>()
            .ForMember(dvm => dvm.Id, d => d.MapFrom(s => s.Id))
            .ForMember(dvm => dvm.Text, d => d.MapFrom(s => s.Text))
            ;
        }
    }


    public class ReportListProfile : Profile
    {
        public ReportListProfile()
        {
            CreateMap<ReportListModel, ReportListViewModel>()
            .ForMember(lvm => lvm.Documents, r => r.MapFrom(s => s.Documents))
            .ForMember(lvm => lvm.Messages, r => r.MapFrom(s => s.Messages))
            .ForMember(lvm => lvm.Masrafs, r => r.MapFrom(s => s.Masrafs))
            ;
        }
    }

    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventViewModel>()
            .ForMember(lvm => lvm.Start, r => r.MapFrom(s => s.Date))
            .ForMember(lvm => lvm.Id, r => r.MapFrom(s => s.Id))
            .ForMember(lvm => lvm.RememberDate, r => r.MapFrom(s => s.RememberDate))
            .ForMember(lvm => lvm.Title, r => r.MapFrom(s => s.Title))
            .ForMember(lvm => lvm.ReminderJobId, r => r.MapFrom(s => s.ReminderJobId))
            ;
        }
    }

    public class NotificationProfile:Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification,NotificationViewModel>()
            .ForMember(lvm=>lvm.Id,r=>r.MapFrom(s=>s.Id))
            .ForMember(lvm=>lvm.Message,r=>r.MapFrom(s=>s.Message))
            .ForMember(lvm=>lvm.IsRead,r=>r.MapFrom(s=>s.IsRead))
            ;
            
        }

    }

}