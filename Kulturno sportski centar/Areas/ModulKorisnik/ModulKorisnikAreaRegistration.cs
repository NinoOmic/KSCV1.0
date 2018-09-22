using System.Web.Mvc;

namespace Kulturno_sportski_centar.Areas.ModulKorisnik
{
    public class ModulKorisnikAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulKorisnik";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulKorisnik_default",
                "ModulKorisnik/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}