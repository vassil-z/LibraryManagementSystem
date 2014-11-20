using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.ViewModels.Roles;
using LibraryManagementSystem.ViewModels.Pager;
using LibraryManagementSystem.ViewModels.AuthenticatingActions;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.Repositories;
using LibraryManagementSystem.DataAccess.DataAccessLayer;
using DataAccess;

namespace LibraryManagementSystem.Controllers
{
    [AuthenticationFilter]
    public class RolesController : BaseController
    {
        public ActionResult Index()
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RolesRepository rolesRepository = new RolesRepository(context);
            RolesIndexVM model = new RolesIndexVM();
            this.TryUpdateModel(model);

            model.RolesPager = model.RolesPager ?? new GenericPagerVM();
            model.RolesPager.CurrentPage = model.RolesPager.CurrentPage == 0 ? 1 : model.RolesPager.CurrentPage;

            model.RolesPager.PagesCount = this.GetPagesCount();
            model.RolesList = rolesRepository.GetAll(model.RolesPager.CurrentPage, ApplicationConfiguration.ItemsPerPage);

            model.RolesPager.Prefix = "RolesPager";
            model.RolesPager.Action = "Index";
            model.RolesPager.Controller = "Roles";
            model.RolesPager.CurrentParameters = new Dictionary<string, object>()
            {
                {"RolesPager.CurrentPage",model.RolesPager.CurrentPage}
            };

            return View(model);
        }

        public ActionResult EditRole(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RolesRepository rolesRepository = new RolesRepository(context);
            AuthenticatingActionsRepository authenticatingActionsRepository = new AuthenticatingActionsRepository(context);

            RolesEditRoleVM model = new RolesEditRoleVM();
            if (id > 0)
            {
                Role role = rolesRepository.GetAll(filter: r => r.ID == id, includeProperties: "AuthenticatingActions").FirstOrDefault();
                PopulateAssignedAuthenticatingActions(role, authenticatingActionsRepository);

                model.ID = role.ID;
                model.Name = role.Name;
            }
            else
            {
                Role role = new Role();
                role.AuthenticatingActions = new List<AuthenticatingAction>();
                PopulateAssignedAuthenticatingActions(role, authenticatingActionsRepository);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RolesEditRoleVM model, string[] assignedAuthenticatingActions)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();                

            Role role = null;
            if (!this.ModelState.IsValid)
            {                
                AuthenticatingActionsRepository authenticatingActionsRepository = new AuthenticatingActionsRepository(context);

                var authenticatingActions = authenticatingActionsRepository.GetAll();
                List<AuthenticatingActionsVM> authenticatingActionsViewModel = new List<AuthenticatingActionsVM>();

                foreach (var action in authenticatingActions)
                {
                    authenticatingActionsViewModel.Add(new AuthenticatingActionsVM
                    {
                        ID = action.ID,
                        Name = action.Name,
                        IsAssigned = false
                    });
                }

                ViewBag.AuthenticatingActions = authenticatingActionsViewModel;
                return View(model);
            }
            
            using (UnitOfWork unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    var authenticatingActionsRepository = new AuthenticatingActionsRepository(unitOfWork);

                    var rolesRepository = new RolesRepository(unitOfWork);
                    if (model.ID > 0)
                    {
                        role = rolesRepository.GetAll(filter: r => r.ID == model.ID, includeProperties: "AuthenticatingActions").FirstOrDefault();
                    }
                    else
                    {
                        role = new Role();
                        role.AuthenticatingActions = new List<AuthenticatingAction>();
                    }

                    role.Name = model.Name;

                    UpdateAuthenticatingActions(assignedAuthenticatingActions, role, authenticatingActionsRepository);
                    rolesRepository.Save(role);
                    PopulateAssignedAuthenticatingActions(role, authenticatingActionsRepository);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.RollBack();
                    throw ex;
                }
            }            

            return RedirectToAction("Index", "Roles");
        }

        public void PopulateAssignedAuthenticatingActions(Role role, AuthenticatingActionsRepository authenticatingActionsRepository)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RolesRepository rolesRepository = new RolesRepository(context);

            var authenticatingActions = authenticatingActionsRepository.GetAll();
            var roleAuthenticatingActions = new HashSet<int>(role.AuthenticatingActions.Select(r => r.ID));
            var model = new List<AuthenticatingActionsVM>();

            foreach (var action in authenticatingActions)
            {
                model.Add(new AuthenticatingActionsVM
                {
                    ID = action.ID,
                    Name = action.Name,
                    IsAssigned = roleAuthenticatingActions.Contains(action.ID)
                });
            }

            ViewBag.AuthenticatingActions = model;
        }

        public void UpdateAuthenticatingActions(string[] assignedAuthenticatingActions, Role role, AuthenticatingActionsRepository authenticatingActionsRepository)
        {
            if (assignedAuthenticatingActions == null)
            {
                return;
            }

            var assignedAuthenticatingActionsHS = new HashSet<string>(assignedAuthenticatingActions);
            var roleAuthenticatingActionsHS = new HashSet<int>(role.AuthenticatingActions.Select(r => r.ID));

            foreach (var action in authenticatingActionsRepository.GetAll())
            {
                if (assignedAuthenticatingActionsHS.Contains(action.ID.ToString()))
                {
                    if (!roleAuthenticatingActionsHS.Contains(action.ID))
                    {
                        role.AuthenticatingActions.Add(action);
                    }
                }
                else
                {
                    if (roleAuthenticatingActionsHS.Contains(action.ID))
                    {
                        role.AuthenticatingActions.Remove(action);
                    }
                }
            }
        }

        public ActionResult DeleteRole(int id)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RolesRepository rolesRepository = new RolesRepository(context);

            RolesDeleteRoleVM model = new RolesDeleteRoleVM();
            Role role = rolesRepository.GetAll(filter: r => r.ID == id, includeProperties: "AuthenticatingActions").FirstOrDefault();
            model.ID = role.ID;
            model.Name = role.Name;

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteRole(RolesDeleteRoleVM model)
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RolesRepository rolesRepository = new RolesRepository(context);                      

            Role role = rolesRepository.GetAll(filter: r => r.ID == model.ID, includeProperties: "AuthenticatingActions").FirstOrDefault();           
            if (role == null)
            {
                return HttpNotFound();
            }

            role.AuthenticatingActions = null;
            rolesRepository.Delete(role);

            return RedirectToAction("Index", "Roles");
        }

        public int GetPagesCount()
        {
            LibraryManagementSystemContext context = new LibraryManagementSystemContext();
            RolesRepository rolesRepository = new RolesRepository(context);

            int pagesCount = 0;
            int rolesCount = rolesRepository.Count();
            int itemsPerPage = ApplicationConfiguration.ItemsPerPage;
            pagesCount = rolesCount / itemsPerPage;
            if ((rolesCount % itemsPerPage) > 0)
            {
                pagesCount++;
            }
            return pagesCount;
        }
    }
}