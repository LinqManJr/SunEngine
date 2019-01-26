using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunEngine.Commons.Models;
using SunEngine.Services.Admin;
using SunEngine.Stores;

namespace SunEngine.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class GroupsAdminController : BaseController
    {
        private readonly GroupsAdminService groupsAdminService;
        //private readonly ICategoriesStore categoriesStore;
        private readonly IUserGroupStore userGroupStore;
        
        public GroupsAdminController(
            UserManager<User> userManager,
            GroupsAdminService groupsAdminService,
            //ICategoriesStore categoriesStore,
            IUserGroupStore userGroupStore) : base(userManager)
        {
            this.groupsAdminService = groupsAdminService;
            //this.categoriesStore = categoriesStore;
            this.userGroupStore = userGroupStore;
        }

        public async Task<IActionResult> LoadJson(string json)
        {
            await groupsAdminService.LoadUserGroupsFromJsonAsync(json);

            //categoriesStore.Reset();
            userGroupStore.Reset();
            
            return Ok();
        }
    }
}