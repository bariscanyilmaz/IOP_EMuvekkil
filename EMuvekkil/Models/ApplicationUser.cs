using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EMuvekkil.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser:IdentityUser
{
    public string IdentityNumber { get; set; }              
    public string NameSurname { get; set; }

    [ForeignKey("Company")]
    public int? CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public IList<EventUsers> EventUsers { get; set; }
    

}