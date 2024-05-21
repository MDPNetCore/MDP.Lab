using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDP.BlazorCore.Lab
{
    public class UserHandler : InteropHandler
    {
        // Methods
        [AllowAnonymous]
        [InteropRoute("/Sites/User/FindById")]
        public async Task<FindByIdResultModel> FindById(FindByIdActionModel actionModel)
        {
            return await Task.FromResult(new FindByIdResultModel());
        }

        public class FindByIdActionModel
        {
            // Properties

        }

        public class FindByIdResultModel
        {
            // Properties
            public string UserId { get; set; } = "U0001";
        }
    }
}
