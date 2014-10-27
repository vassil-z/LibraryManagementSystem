using System.Web.Mvc;
using LibraryManagementSystem.Filters;

namespace LibraryManagementSystem.Controllers
{
    [LoginFilter]
    public class BaseController : Controller
    {
    }
}